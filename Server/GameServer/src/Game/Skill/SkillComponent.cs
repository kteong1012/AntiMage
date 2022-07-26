﻿using Cfg;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace PostMainland
{
    public struct SkillsComponent : IEcsAutoReset<SkillsComponent>
    {
        public List<int> Skills { get; set; }

        public void AutoReset(ref SkillsComponent c)
        {
            c.Skills = new List<int>();
        }
    }
    public struct AT_CastSkill : IEcsAutoReset<AT_CastSkill>
    {
        public int Caster { get; set; }
        public int SkillId { get; set; }
        public List<int> Targets { get; set; }

        public void AutoReset(ref AT_CastSkill c)
        {
            c.Targets = new List<int>();
        }
    }
}
