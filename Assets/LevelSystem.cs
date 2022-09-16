using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level;
    private int experience;
    private int experienceToNextLevel;
    public float EXPscaling;

    // Start is called before the first frame update
    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel) levelUp();
        UIManager.Instance.UpdateExperience(experience);
    }

    private void levelUp()
    {
        experience -= experienceToNextLevel;
        level++;
        UIManager.Instance.UpdateLevel(level + 1);
        experienceCalculator();
    }

    //set level method
    public void SetLevel(int newLevel)
    {
        level = newLevel;
        experienceCalculator();
    }

    private void experienceCalculator()
    {
        // Calculate exp for next level
        experienceToNextLevel = (int)(100 * Mathf.Pow(EXPscaling, level));

        // round it down to closest 10
        experienceToNextLevel -= experienceToNextLevel % 10;
        //print("experience To Level " + level + " is: " + experienceToNextLevel);
    }
}
