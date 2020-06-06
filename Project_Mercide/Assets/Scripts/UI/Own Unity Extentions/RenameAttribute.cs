using UnityEngine;

// Source: http://answers.unity.com/answers/1487948/view.html

public class RenameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public RenameAttribute(string name)
    {
        NewName = name;
    }
}