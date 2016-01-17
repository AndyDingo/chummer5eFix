﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chummer.Skills;

namespace Chummer.UI.Shared
{
	public partial class KnowledgeSkillControl : UserControl
	{
		private static double total = 0;
		private KnowledgeSkill skill;
		public KnowledgeSkillControl(KnowledgeSkill skill)
		{
			this.skill = skill;
			InitializeComponent();
			if (skill.CharacterObject.Created)
			{
				//TODO: Change display to play mode
			}
			else
			{
				//Display
				lblModifiedRating.DataBindings.Add("Text", skill, nameof(Skill.DisplayPool), false, DataSourceUpdateMode.OnPropertyChanged);

				//Up down boxes
				nudKarma.DataBindings.Add("Value", skill, nameof(Skill.Karma), false, DataSourceUpdateMode.OnPropertyChanged);
				nudSkill.DataBindings.Add("Value", skill, nameof(Skill.Base), false, DataSourceUpdateMode.OnPropertyChanged);

				nudSkill.DataBindings.Add("Enabled", skill, nameof(Skill.BaseUnlocked), false,
					DataSourceUpdateMode.OnPropertyChanged);

				if (skill.CharacterObject.BuildMethod.HaveSkillPoints() || skill.CharacterObject.Options.FreeKarmaKnowledge)
				{
					chkKarma.DataBindings.Add("Checked", skill, nameof(Skill.BuyWithKarma), false,
						DataSourceUpdateMode.OnPropertyChanged);
				}
				else
				{
					chkKarma.Visible = false;
				}

				cboType.DataSource = KnowledgeSkill.KnowledgeTypes;
				cboType.DisplayMember = nameof(ListItem.Name);
				cboType.ValueMember = nameof(ListItem.Value);
				cboType.DataBindings.Add("SelectedValue", skill, nameof(KnowledgeSkill.Type), false, DataSourceUpdateMode.OnPropertyChanged);
				

				cboSkill.DataSource = skill.KnowledgeSkillCatagories;
				cboSkill.DisplayMember = nameof(ListItem.Name);
				cboSkill.ValueMember = nameof(ListItem.Value);
				cboSkill.SelectedIndex = -1;
				cboSkill.DataBindings.Add("Text", skill, nameof(KnowledgeSkill.WriteableName), false, DataSourceUpdateMode.OnPropertyChanged);
				


				//dropdown/spec
				cboSpec.DataSource = skill.CGLSpecializations;
				cboSpec.DisplayMember = nameof(ListItem.Name);
				cboSpec.ValueMember = nameof(ListItem.Value);
				cboSpec.SelectedIndex = -1;
				
				cboSpec.DataBindings.Add("Enabled", skill, nameof(Skill.Leveled), false, DataSourceUpdateMode.OnPropertyChanged);
				cboSpec.DataBindings.Add("Text", skill, nameof(Skill.Specialization), false, DataSourceUpdateMode.OnPropertyChanged);

				skill.PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == nameof(Skill.CGLSpecializations))
					{
						cboSpec.DataSource = null;
						cboSpec.DataSource = skill.CGLSpecializations;
						cboSpec.DisplayMember = nameof(ListItem.Name);
						cboSpec.ValueMember = nameof(ListItem.Value);
						cboSpec.MaxDropDownItems = Math.Max(1, skill.CGLSpecializations.Count);
					}
				};
			}


			cmdDelete.Click += (sender, args) => { skill.CharacterObject.KnowledgeSkills.Remove(skill); };



		}

		private void KnowledgeSkillControl_Load(object sender, EventArgs e)
		{
			
		}

		private void KnowledgeSkillControl_DoubleClick(object sender, EventArgs e)
		{

		}
	}
}
