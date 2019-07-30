﻿using ASU2019_NetworkedGameWorkshop.controller;
using ASU2019_NetworkedGameWorkshop.model.character;
using ASU2019_NetworkedGameWorkshop.model.character.types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASU2019_NetworkedGameWorkshop.model.Shop {
    class ShopUIPanel:Panel {
        private readonly FlowLayoutPanel statLabels;
        private readonly GameManager manager;
        private readonly Label lbl_statsNames;
        private readonly Label lbl_statsChanges;
        Character selected;

        private Label lbl_statsValues;

        public ShopUIPanel(GameForm gameForm,GameManager gameManager) {
            manager = gameManager;

            this.Size = new Size(270, 300);
            this.Location = new Point((int)(gameForm.Width * 0.78), (int)(gameForm.Height * 0.05));
            this.BackColor = Color.White;
            this.Visible = true;
            this.Padding = new Padding(10, 10, 10, 10);

            statLabels = new FlowLayoutPanel {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Top
            };


            lbl_statsNames = new Label {
                Dock = DockStyle.Left,
                Text = "Character Class:\nHealth Points:\nRange:\nAtk Damage:\nAtk Speed:\nArmour:\nMagic Resist:",
                AutoSize = true,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };

            lbl_statsValues = new Label {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };
            lbl_statsChanges = new Label {
                Dock = DockStyle.Right,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };
            statLabels.Controls.Add(lbl_statsNames);
            statLabels.Controls.Add(lbl_statsValues);
            statLabels.Controls.Add(lbl_statsChanges);
            gameForm.Controls.Add(this);
        }
        private void GetCharStatsPanel() {
            if (manager.SelectedTile.CurrentCharacter != null) {
                selected = manager.SelectedTile.CurrentCharacter;

                lbl_statsValues.Text = $"{selected.CharacterType.Name}\n{selected.Stats[StatusType.HealthPointsMax]}\n{selected.Stats[StatusType.Range]}\n{selected.Stats[StatusType.AttackDamage]}\n{selected.Stats[StatusType.AttackSpeed]}\n{selected.Stats[StatusType.Armor]}\n{selected.Stats[StatusType.MagicResist]}";
                lbl_statsValues.AutoSize = true;
            }
        }
        public void ShowCharStats() {
            GetCharStatsPanel();
            this.Controls.Add(statLabels);
        }
        public void ShowStatsChanges() {
            UpdateChanges();
            lbl_statsChanges.Visible = true;
        }
        public void HideChanges() {
            lbl_statsChanges.Visible = false;
        }
        public void UpdateChanges() {
            CharacterType nextLevel = selected.characterType[selected.CurrentLevel + 1];
            String changesHP = (selected.Stats[StatusType.HealthPointsMax] != nextLevel[StatusType.HealthPointsMax] ? $"--> {nextLevel[StatusType.HealthPointsMax]}\n" : "\n");
            String changesRange = (selected.Stats[StatusType.Range] != nextLevel[StatusType.Range] ? $"--> {nextLevel[StatusType.Range]}\n" : "\n");
            String changesDmg = (selected.Stats[StatusType.AttackDamage] != nextLevel[StatusType.AttackDamage] ? $"--> {nextLevel[StatusType.AttackDamage]}\n" : "\n");
            String changesSpd = (selected.Stats[StatusType.AttackSpeed] != nextLevel[StatusType.AttackSpeed] ? $"--> {nextLevel[StatusType.AttackSpeed]}\n" : "\n");
            String changesArmour = (selected.Stats[StatusType.Armor] != nextLevel[StatusType.Armor] ? $"--> {nextLevel[StatusType.Armor]}\n" : "\n");
            String changesResist = (selected.Stats[StatusType.MagicResist] != nextLevel[StatusType.MagicResist] ? $"--> {nextLevel[StatusType.MagicResist]}\n" : "\n");
            lbl_statsChanges.Text = "\n" + changesHP + changesRange + changesDmg + changesSpd + changesArmour + changesResist;
        }
    }
}
