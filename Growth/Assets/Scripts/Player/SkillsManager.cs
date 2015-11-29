using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsManager {

    public enum SkillColor { Red };

    private SkillColor m_currentSkill = SkillColor.Red;
    private SortedDictionary<SkillColor, float> m_skillStrengths;
    private const float m_fBaseSkillStrength = 1.0f;

    public SkillColor CurrentSkill()
    {
        return m_currentSkill;
    }

    public float CurrentSkillStrength()
    {
        return m_skillStrengths.ContainsKey(m_currentSkill) ? m_skillStrengths[m_currentSkill] : 0;
    }

    private void InitializeSkills()
    {
        m_skillStrengths = new SortedDictionary<SkillColor, float>();
        m_skillStrengths.Add(m_currentSkill, m_fBaseSkillStrength);
    }

    public SkillsManager()
    {
        InitializeSkills();
    }
}
