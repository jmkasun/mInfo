using DBCore.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahamewnawaInfo.Reporting
{
    public partial class frm_CustomReport : Form
    {
        public frm_CustomReport()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter1Combo.DataSource = null;
            filter1Combo.Items.Clear();
            filter1FromDate.Visible = filter1ToDate.Visible = filter1Text.Visible = filter1Combo.Visible = false;
            ComboIndexChanged((ComboBox)sender);
        }

        private void ComboIndexChanged(ComboBox combo)
        {
            switch (combo.Text)
            {
                case "පැවිදි වූ දිනය":
                case "උපසම්පදා වූ දිනය":
                case "උපන් දිනය":
                    {
                        if (combo == comboBox1)
                        {
                            filter1FromDate.Visible = filter1ToDate.Visible = true;
                        }
                        else
                        {
                            filter2FromDate.Visible = filter2Todate.Visible = true;
                        }
                        return;
                    }

                case "උපසම්පදා/සාමනේර":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddBhikkuType(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddBhikkuType(filter2Combo);
                        }
                        return;
                    }

                case "පැවිදි වූ විහාරයේ නම":
                    {

                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddRobbingTemple(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddRobbingTemple(filter2Combo);
                        }
                        return;
                    }

                case "උපසම්පදා කළ ස්ථානය":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddUpasampadaTemple(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddUpasampadaTemple(filter2Combo);
                        }
                        return;
                    }

                case "තනතුර":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddBhikkuPosts(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddBhikkuPosts(filter2Combo);
                        }
                        return;
                    }

                case "උපන් රට":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddCountry(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddCountry(filter2Combo);
                        }
                        return;
                    }

                case "හැකියාවන්":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddAbilities(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddAbilities(filter2Combo);
                        }
                        return;
                    }

                case "භාෂා හැකියාවන්":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddLanguages(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddLanguages(filter2Combo);
                        }
                        return;
                    }

                case "වර්තමානයේ ...":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddBhikkuCurrentStatus(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddBhikkuCurrentStatus(filter2Combo);
                        }
                        return;
                    }

                case "ලේ වර්ගය":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddBloodGroups(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddBloodGroups(filter2Combo);
                        }
                        return;
                    }

                case "වැඩසිටින අසපුව":
                    {
                        if (combo == comboBox1)
                        {
                            filter1Combo.Visible = true;
                            AddAsapuwa(filter1Combo);
                        }
                        else
                        {
                            filter2Combo.Visible = true;
                            AddAsapuwa(filter2Combo);
                        }
                        return;
                    }

                case "-------- N/A --------":
                    {
                       
                        return;
                    }

                default:
                    {
                        if (combo == comboBox1)
                        {
                            filter1Text.Visible = true;
                        }
                        else
                        {
                            filter2text.Visible = true;
                        }
                        return;
                    }
            }
        }

        private void AddBloodGroups(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "A+", "B+", "O+", "AB+", "A-", "B-", "AB-", "O-" });
        }

        private void AddAbilities(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "ධර්ම දේශනා", "වන්දනා", "සජ්ඣායනා"});
        }

        private void AddBhikkuType(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "උපසම්පදා", "සාමනේර"});
        }

        private void AddBhikkuPosts(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "සංඝෝපස්තායක", "අනු සංඝෝපස්තායක" });
        }

        private void AddBhikkuCurrentStatus(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "සිටී", "වෙනත් ස්ථානයක සිටී", "ශිෂ්‍ය භාවයෙන් ඉවත් වී සිටී", "උපැවිදි වී ඇත", "අපවත් වී ඇත" });
        }

        private void AddLanguages(ComboBox combo)
        {
            combo.Items.AddRange(new string[] { "සිංහල","දෙමළ","ඉංග්‍රීසි","හින්දි","" });
        }


        private void AddRobbingTemple(ComboBox combo)
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(combo, DBCore.UtilityDataName.placeRobing);
            }
        }

        private void AddUpasampadaTemple(ComboBox combo)
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(combo, DBCore.UtilityDataName.PlaceUpasampada);
            }
        }

        private void AddCountry(ComboBox combo)
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(combo, DBCore.UtilityDataName.Country);
            }
        }


        private void AddAsapuwa(ComboBox combo)
        {
            using (Asapuwa ut = new Asapuwa(true))
            {
                ut.BindToCombo(combo);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter2Combo.DataSource = null;
            filter2Combo.Items.Clear();

            filter2FromDate.Visible = filter2Todate.Visible = filter2text.Visible = filter2Combo.Visible = false;
            ComboIndexChanged((ComboBox)sender);
        }
    }
}
