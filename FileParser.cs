using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace parser
{

    public class FeatureRegion
    {
        public string Comment { get; set; }
        public Feature Feature { get; set; }
        public FeatureRegion(Feature feature, string comment)
        {
            Feature = feature;
            Comment = comment;
        }
    }

    public class FoundMatch
    {
        public string Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public List<FeatureRegion> Features { get; set; } = new List<FeatureRegion>();

        public FoundMatch(string value, int start, int end)
        {
            Value = value;
            Start = start;
            End = start + end;
        }

        public string GetFeaturesCSV(string EntryID, List<string> selTypes)
        {
            string res = "";
            foreach (FeatureRegion fr in Features)
            {
                if (selTypes.Contains(fr.Feature.Type))
                    res += fr.Feature.Description + " - " + fr.Feature.Type + ":" + fr.Feature.Start.Value.ToString() + "-" + fr.Feature.End.Value.ToString();
            }
            res = res.Trim('^').Trim();
            if (res == "")
                return "";
            return " [" + res.Replace("^", ", ") + "]";
        }

        public string GetFeatures(string EntryID, List<string> selTypes)
        {
            string res = "";
            foreach (FeatureRegion fr in Features)
            {
                if (selTypes.Contains(fr.Feature.Type))
                    res += "<a href='open_features/" + EntryID + "/"+fr.Feature.InternalID+"' title='" + fr.Feature.Type + ": "+fr.Feature.Start.Value.ToString()+"-"+ fr.Feature.End.Value.ToString() + "'>" + fr.Feature.Description + "</a>^";
            }
            res = res.Trim('^').Trim();
            if (res == "")
                return "";
            return " <i>[" + res.Replace("^", ", ") + "]</i>";
        }    
    }

    public class Protein
    {
        public string EntryID { get; set; }
        public string Entry { get; set; }
        public string Sequence { get; set; }        
        public List<FoundMatch> FoundSequences { get; set; } = new List<FoundMatch>();
        public List<Feature> Features { get; set; } = new List<Feature>();

        public string FoundSequence(List<string> selectedTypes) 
        { 
            string res = "";
            foreach (FoundMatch match in FoundSequences)
            {
                res += "<b>" + match.Value + "</b>";
                res += match.GetFeatures(EntryID, selectedTypes);
                res += "^";
            }
            res = res.Trim('^').Trim();
            return res.Replace("^", "<br/>");             
        }

        public string FoundSequenceCSV(List<string> selectedTypes)
        {
            string res = "";
            foreach (FoundMatch match in FoundSequences)
            {
                res += match.Value;
                res += match.GetFeaturesCSV(EntryID, selectedTypes);
                res += "^";
            }
            res = res.Trim('^').Trim();
            return res.Replace("^", "; ");
        }

        public string FoundInFeatures
        {
            get
            {
                string res = "";
                foreach (FoundMatch match in FoundSequences)
                {
                    foreach (FeatureRegion fr in match.Features)
                    {
                        res += fr.Feature.Type + "^";
                    }
                }
                res = res.Trim('^').Trim();
                return res.Replace("^", ", ");
            }
        }


        public string BreakSequence(int n)
        {
            int k = 1;
            if (n == 0 || Sequence.Length < n)
            {
                n = Sequence.Length;
                k = 0;
            }

            string result = Sequence.Substring(Sequence.Length - n);

            for (int i=FoundSequences.Count()-1; i>=0; i--)
            {
                result = result.Substring(0, FoundSequences[i].Start) + "<span style='background-color:#aee500;border-left:1px solid #444; border-right:1px solid #444;'>" + FoundSequences[i].Value + "</span>" + result.Substring(FoundSequences[i].End);
            }
            if (k == 0)
                return result;
            else
                return " ... " + result;
        }

        public string BreakSequence()
        {
            if (Sequence.Length < 50)
                return Sequence;

            return " ... " + Sequence.Substring(Sequence.Length - 50);
        }

        public Protein(string text)
        {
            string[] fields = text.Split('|');

            for (int i = 0; i < fields.Length; i++)
            {
                string s = fields[i].Trim();
                if (s == "tr")
                    continue;
                if (s.Contains("OS=") || s.Contains("GN=") || s.Contains("OX="))
                {
                    string[] seq = s.Split('\n');
                    Entry = seq[0];
                    for (int j = 1; j < seq.Length; j++)
                    {
                        Sequence += seq[j];
                    }                        
                }
                else
                    EntryID = s;
            }
        }
    }

    internal class FileParser
    {
        public List<Protein> Parse(string text, List<FeatureCollection> features)
        {
            string[] lines = text.Split('>');
            List<Protein> proteins = new List<Protein>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                Protein p = new Protein(line);
                if (!string.IsNullOrEmpty(p.Sequence))
                {
                    proteins.Add(p);
                    FeatureCollection fc = FeatureCollection.Exists(p.EntryID, features);
                    if (fc != null)
                        p.Features = fc.Features;
                    else
                        p.Features = new List<Feature>();
                }
            }
            return proteins;
        }

        public List<Protein> Search(List<Protein> proteins, string text, decimal n, List<string> selectedTypes)
        {
            List<Protein> results = new List<Protein>();
            foreach (Protein protein in proteins)
            {
                if (IsMatch(protein, text, n, selectedTypes))
                {
                    foreach (FoundMatch match in protein.FoundSequences)
                    {
                        foreach (FeatureRegion fr in match.Features)
                        {
                            if (selectedTypes.Contains(fr.Feature.Type))
                            {
                                results.Add(protein);
                                break;
                            }
                        }
                    }
                }
            }
            return results;
        }

        public List<Protein> QuickSearch(List<Protein> proteins, string text, decimal n)
        {
            List<Protein> results = new List<Protein>();

            foreach (Protein protein in proteins)
            {
                var seq = protein.Sequence;
                if (protein.Sequence.Length > (int)n && n>0)
                    seq = protein.Sequence.Substring(protein.Sequence.Length - (int)n);

                if (seq.Contains(text))
                    results.Add(protein);
            }
            return results;
        }

        public string GetHTML(List<Protein> proteins, int n, string ptrn, List<string> selectedTypes)
        {
            string col = "#B0E0E6";
            bool alt = false;
            string featureTypes = "";
            foreach (string type in selectedTypes)
                featureTypes += type + ", ";
            featureTypes = featureTypes.Trim().Trim(',');
            string what = "last "+n.ToString();
            if (n==0)
            {
                what = "all";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body style='font-family:Calibri; font-size:12pt;'><table style='width:100%;border:1px;'>");
            sb.Append("<div style='background-color:#B0E0E6;height:64px;'>Found Entries: " + proteins.Count().ToString()+ "<br/>Searched "+ what +" characters; Pattern " + ptrn + 
                "<br/>Considered Feature Types: " + featureTypes +
                "</div><div style='overflow-y:scroll;'>");
            foreach (Protein protein in proteins)
            {
                if (alt)
                    sb.Append("<tr style='background-color:" + col + ";'>");
                else
                    sb.Append("<tr>");

                string cnt = "0";
                if (protein.Features != null)
                    cnt = protein.Features.Count.ToString();

                sb.Append("<td style='width:10%;'><a style='color:blue;' href='https://www.uniprot.org/uniprotkb/"+protein.EntryID+ "/entry'>");
                sb.Append(protein.EntryID);
                sb.Append("</a></td><td style='width:40%;'><a href='open_features/" + protein.EntryID + "'>Features [" + cnt + "]</a></br/>");
                sb.Append(protein.Entry);
                sb.Append("</td><td style='width:20%;font-family:Courier New;'>");
                sb.Append(protein.FoundSequence(selectedTypes));
                sb.Append("</td><td><a style='color:blue;font-family:Courier New;' href='https://www.uniprot.org/uniprotkb/" + protein.EntryID+ "/entry#sequences'>");
                sb.Append(protein.BreakSequence(n));
                sb.Append("</a></td></tr>");
                alt = !alt;
            }
            sb.Append("</table></div></body></html>");
            return sb.ToString();
        }

        public string GetHTML(List<Protein> proteins, ref ToolStripProgressBar bar)
        {
            string col = "#B0E0E6";
            bool alt = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body style='font-family:Calibri; font-size:12pt;'><table style='width:100%;border:1px;'>");
            sb.Append("<div style='background-color:#B0E0E6;height:64px;'>Total Entries: " + proteins.Count().ToString() + "</div><div style='overflow-y:scroll;'>");
            foreach (Protein protein in proteins)
            {
                if (alt)
                    sb.Append("<tr style='background-color:" + col + ";'>");
                else
                    sb.Append("<tr>");

                string cnt = "0";
                if (protein.Features != null)
                    cnt = protein.Features.Count.ToString();

                sb.Append("<td style='width:10%;'><a style='color:blue;' href='https://www.uniprot.org/uniprotkb/" + protein.EntryID + "/entry'>");
                sb.Append(protein.EntryID);
                sb.Append("</a></td><td style='width:50%;'><a href='open_features/" + protein.EntryID + "'>Features [" + cnt + "]</a></br/>");
                sb.Append(protein.Entry);                
                sb.Append("</td><td><a style='color:blue;font-family:Courier New;' href='https://www.uniprot.org/uniprotkb/" + protein.EntryID + "/entry#sequences'>");
                sb.Append(protein.BreakSequence());
                sb.Append("</a></td></tr>");
                alt = !alt;
                bar.Value++;
                Application.DoEvents();
            }
            sb.Append("</table></div></body></html>");
            return sb.ToString();
        }


        private bool IsMatch(Protein protein, string pattern, decimal n, List<string> selTypes)
        {
            try
            {
                if (string.IsNullOrEmpty(protein.Sequence))
                    return false;
                
                var text = protein.Sequence;
                if (n>0 && protein.Sequence.Length > (int)n)
                    text = protein.Sequence.Substring(protein.Sequence.Length - (int)n);
                long delta = protein.Sequence.Length - text.Length;

                var p = pattern.Replace("*", "(.*?)").Replace("@", ".");

                MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(text, p, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                protein.FoundSequences.Clear();

                foreach (Match match in matches)
                {
                    FoundMatch f = new FoundMatch(match.Value, match.Index, match.Length);
                    if (protein.Features == null)
                        protein.Features = new List<Feature>();

                    foreach (Feature feature in protein.Features)
                    {
                        if (selTypes.Contains(feature.Type))
                        {
                            if (feature.Start.Value <= delta + match.Index + match.Length && feature.End.Value >= delta + match.Index + match.Length)
                            {
                                FeatureRegion fr = new FeatureRegion(feature, "");
                                f.Features.Add(fr);
                            }
                        }
                    }
                    if (f.Features.Count > 0)
                        protein.FoundSequences.Add(f);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return protein.FoundSequences.Count()>0;
        }

        public void ExportToCSV(List<Protein> founds, string FileName, List<string> selTypes, string ptrn, long n)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Found Entries: " + founds.Count().ToString()); 
            sb.AppendLine("Searched last " + n.ToString() + " characters");
            sb.AppendLine("Pattern " + ptrn);
            string sTypes = "";
            foreach (string s in selTypes)
            { 
                sTypes += s + ";";  
            }
            sTypes.TrimEnd().TrimEnd(';');

            sb.AppendLine("Considered Feature Types: " + sTypes);
            sb.AppendLine("");
            sb.AppendLine("EntryID,Entry,FoundSequence,Sequence");
            foreach (Protein protein in founds)
            {
                sb.AppendLine(protein.EntryID + "," + protein.Entry.Replace(",", ";") + "," + protein.FoundSequenceCSV(selTypes) + "," + protein.Sequence);
            }
            System.IO.File.WriteAllText(FileName, sb.ToString());
        }

        public void ExportToHTML(WebBrowser browser, string FileName)
        {
            string html = browser.DocumentText;
            System.IO.File.WriteAllText(FileName, html);
        }
        public void Overlapp(ref List<Protein> result, List<Protein> founds)
        {
            foreach (Protein protein in founds)
            {
                if (!result.Contains(protein))
                    result.Add(protein);
            }
        }

        public static string GenerateFileName(string pattern, string combo = "")
        {
            string res = pattern.Replace("*", "x").Replace(" ", "").Replace(",", "").Replace(";", "").Replace(":", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", "").Replace("?", "").Replace("!", "").Replace("=", "").Replace("+", "").Replace("-", "").Replace("_", "").Replace("/", "").Replace("\\", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("\"", "").Replace("'", "");
            if (combo!="")
                res = combo.Replace(" ", "_") + "_" + res;

            return res;
        }
    }
}
