using System;
using System.Windows.Forms;
using MHXXSaveEditor.Data;

namespace MHXXSaveEditor.Forms
{
    public partial class EditTalismanDialog : Form
    {
        private MainForm mainForm;

        public EditTalismanDialog(MainForm mainForm, string eqpName)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Text = "编辑护石 - " + eqpName;
            comboBoxSkillName1.Items.AddRange(GameConstants.SkillNames);
            comboBoxSkillName2.Items.AddRange(GameConstants.SkillNames);

            comboBoxSkillName1.SelectedIndex = Convert.ToInt32(mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 12]);
            comboBoxSkillName2.SelectedIndex = Convert.ToInt32(mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 13]);
            SetMaxMinSkills();
            try
            {
                numericUpDownSkillLevel1.Value = Convert.ToInt32(mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 14]);
                numericUpDownSkillLevel2.Value = Convert.ToInt32(mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 15]);
            }
            catch
            {
                MessageBox.Show("你正在编辑的护石可能无效，游戏内无法使用。请删除它并换一颗新的。", "错误 - 无效护石");
            }
            numericUpDownSlots.Value = Convert.ToInt32(mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 16]);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 12] = Convert.ToByte(comboBoxSkillName1.SelectedIndex);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 13] = Convert.ToByte(comboBoxSkillName2.SelectedIndex);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 14] = Convert.ToByte(numericUpDownSkillLevel1.Value);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 15] = Convert.ToByte(numericUpDownSkillLevel2.Value);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 16] = Convert.ToByte(numericUpDownSlots.Value);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 17] = 0;
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 18] = Convert.ToByte(GameConstants.TalismanRarity[mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 2]]);
            mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 19] = 1;
            for ( int a = 20; a < 36; a++)
            {
                mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + a] = 0;
            }

            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetMaxMinSkills()
        {
            string skillMaxMin1 = "";
            string skillMaxMin2 = "";
            switch (GameConstants.TalismanRarity[mainForm.player.EquipmentInfo[(mainForm.equipSelectedSlot * 36) + 2]]) {
                case 97:
                    skillMaxMin1 = GameConstants.TalismanMysterySkill[comboBoxSkillName1.SelectedIndex];
                    skillMaxMin2 = GameConstants.TalismanMysterySkill[comboBoxSkillName2.SelectedIndex];
                    break;
                case 98:
                    skillMaxMin1 = GameConstants.TalismanShiningSkill[comboBoxSkillName1.SelectedIndex];
                    skillMaxMin2 = GameConstants.TalismanShiningSkill[comboBoxSkillName2.SelectedIndex];
                    break;
                case 99:
                    skillMaxMin1 = GameConstants.TalismanTimewornSkill[comboBoxSkillName1.SelectedIndex];
                    skillMaxMin2 = GameConstants.TalismanTimewornSkill[comboBoxSkillName2.SelectedIndex];
                    break;
                case 100:
                    skillMaxMin1 = GameConstants.TalismanEnduringSkill[comboBoxSkillName1.SelectedIndex];
                    skillMaxMin2 = GameConstants.TalismanEnduringSkill[comboBoxSkillName2.SelectedIndex];
                    break;
            };
            string[] skill1 = skillMaxMin1.Split(':');
            string[] skill1MaxMin = skill1[0].Split('~');
            string[] skill2 = skillMaxMin2.Split(':');
            string[] skill2MaxMin = skill2[1].Split('~');

            numericUpDownSkillLevel1.Minimum = Convert.ToInt32(skill1MaxMin[0]);
            numericUpDownSkillLevel1.Maximum = Convert.ToInt32(skill1MaxMin[1]);
            numericUpDownSkillLevel2.Minimum = Convert.ToInt32(skill2MaxMin[0]);
            numericUpDownSkillLevel2.Maximum = Convert.ToInt32(skill2MaxMin[1]);
        }

        private void ComboBoxSkillName1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetMaxMinSkills();
        }

        private void ComboBoxSkillName2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetMaxMinSkills();
        }
    }
}
