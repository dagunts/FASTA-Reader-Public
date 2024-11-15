using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parser
{
    public class Location
    {
        public long Value { get; set; }
        public string Modifier { get; set; }

        public Location(long value, string modifier)
        {
            Value = value;
            Modifier = modifier;
        }
    }

    public class Evidence
    {
        public string ID { get; set; }
        public string EvidenceCode { get; set; }
        public string Source { get; set; }

        public Evidence(string id, string evidenceCode, string source)
        {
            ID = id;
            EvidenceCode = evidenceCode;
            Source = source;
        }
    }

    public class Feature
    {
        public string InternalID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Location Start { get; set; }
        public Location End { get; set; }
        public List<Evidence> Evidences { get; set; }
        

        public Feature(string json)
        {
            InternalID = Guid.NewGuid().ToString();
            JsonDocument doc = JsonDocument.Parse(json);
            Type = doc.RootElement.GetProperty("type").GetString();
            Description = doc.RootElement.GetProperty("description").GetString();
            if (string.IsNullOrEmpty(Description))
                Description = "N/A";

            if (doc.RootElement.TryGetProperty("evidences", out JsonElement evs))
            {
                Evidences = new List<Evidence>();
                foreach (JsonElement element in evs.EnumerateArray())
                {
                    string id1 = "";
                    if (element.TryGetProperty("id", out JsonElement id))
                        id1 = id.GetString();

                    string evidenceCode1 = "";
                    if (element.TryGetProperty("evidenceCode", out JsonElement evidenceCode))
                        evidenceCode1 = evidenceCode.GetString();

                    string source1 = "";
                    if (element.TryGetProperty("source", out JsonElement source))
                        source1 = source.GetString();                      

                    Evidences.Add(new Evidence(id1, evidenceCode1, source1));
                }
            }   

            JsonElement st = doc.RootElement.GetProperty("location").GetProperty("start");
            JsonElement en = doc.RootElement.GetProperty("location").GetProperty("end");

            long locValue = 0;
            string locModifier = "";

            if (st.TryGetProperty("value", out JsonElement value))
            {
                try { locValue = value.GetInt64(); }
                catch { locValue = 0; }
            }
            if (st.TryGetProperty("modifier", out JsonElement modifier) )
                locModifier = modifier.GetString();

            Start = new Location(locValue, locModifier);

            if (en.TryGetProperty("value", out JsonElement value1))
            {
                try { locValue = value1.GetInt64(); }
                catch { locValue = value.GetInt64(); }
            }
            if (en.TryGetProperty("modifier", out JsonElement modifier1))
                locModifier = modifier1.GetString();

            End = new Location(locValue, locModifier);
        }

    }

    public class FeatureCollection
    {
        public string entryType { get; set; }
        public string primaryAccession { get; set; }

        public List<Feature> Features { get; set; }

        public static FeatureCollection Exists(string proteinid, List<FeatureCollection> features)
        {
            foreach (FeatureCollection fc in features)
            {
                if (fc.primaryAccession == proteinid)
                    return fc;
            }
            return null;
        }

        public static List<FeatureCollection> LoadFeatures(string json, ref List<string> ftTypes, ref ToolStripProgressBar bar)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            bar.Maximum = doc.RootElement.GetProperty("entries").GetArrayLength();
            bar.Value = 0;

            List<FeatureCollection> results = new List<FeatureCollection>();
            ftTypes = new List<string>();

            foreach (JsonElement element in doc.RootElement.GetProperty("entries").EnumerateArray())
            {
                bar.Value++;
                Application.DoEvents();

                FeatureCollection fc = new FeatureCollection();

                fc.entryType = element.GetProperty("entryType").GetString();
                fc.primaryAccession = element.GetProperty("primaryAccession").GetString();

                if (element.TryGetProperty("features", out JsonElement features))
                {
                    fc.Features = new List<Feature>();
                    foreach (JsonElement ele in element.GetProperty("features").EnumerateArray())
                    {
                        fc.Features.Add(new Feature(ele.GetRawText()));
                        if (!ftTypes.Contains(fc.Features.Last().Type))
                            ftTypes.Add(fc.Features.Last().Type);
                    }
                }

                results.Add(fc);
            }
            ftTypes.Sort();
            return results;
        }
    }

    public class FeatureAPI
    {
        private static string GetWebContent(string url)
        {
            string content = "";
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                content = wc.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return content;
        }

        public static string GetFeatures(string proteinid)
        {
            string url = "https://rest.uniprot.org/uniprotkb/"+ proteinid + ".json?fields=ft_var_seq%2Cft_variant%2Cft_non_cons%2Cft_non_std%2Cft_non_ter%2Cft_conflict%2Cft_unsure%2Cft_act_site%2Cft_binding%2Cft_dna_bind%2Cft_site%2Cft_mutagen%2Cft_intramem%2Cft_topo_dom%2Cft_transmem%2Cft_chain%2Cft_crosslnk%2Cft_disulfid%2Cft_carbohyd%2Cft_init_met%2Cft_lipid%2Cft_mod_res%2Cft_peptide%2Cft_propep%2Cft_signal%2Cft_transit%2Cft_strand%2Cft_helix%2Cft_turn%2Cft_coiled%2Cft_compbias%2Cft_domain%2Cft_motif%2Cft_region%2Cft_repeat%2Cft_zn_fing";
            string json = GetWebContent(url);
            json = json.Replace("{", "{\n").Replace("}", "\n}");
            return json;
        }
    }
}
