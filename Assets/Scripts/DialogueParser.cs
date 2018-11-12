using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DialogueParser : MonoBehaviour
{
    StringBuilder sb;

    [HideInInspector]
    public string baseLocation = "Assets/Data/";

    struct DialogueLine
    {
        public bool isChoice;
        public string name;
        public string content;
        public int nextLine;
        public string[] options;

        public DialogueLine(bool _isChoice, string _name, string _content)
        {
            isChoice = _isChoice;
            name = _name;
            content = _content;
            nextLine = 0;
            options = new string[0];
        }
    }

    private int currLine;

    List<DialogueLine> lines;

    // Use this for initialization
    void Awake()
    {
        sb = new StringBuilder();
        currLine = 0;
    }

    public void LoadDialogue(string filename)
    {
        //purge previous chat
        currLine = 0;
        lines = new List<DialogueLine>();

        string line;
        StreamReader r = new StreamReader(baseLocation + filename + ".dlg");

        using (r)
        {
            do
            {
                currLine++;
                line = r.ReadLine();

                if (line != null)
                {
                    //replace [] in line with Character Name
                    if (line.Contains("[]"))
                    {
                        sb.Append(line);
                        line = sb.Replace("[]", "playerName").ToString();
                        sb.Remove(0, sb.Length);
                    }

                    //splitting line into 3 parts
                    //part 0 = character name
                    //part 1 = dialogue modifier
                    //part 2 = dialogue text
                    string[] part = line.Split('|');

                    //set nextLine based on dialogue modifier
                    int nextLine;

                    if (part[1] == "Choice")
                    {
                        //does nothing?
                        nextLine = currLine - 1;
                    }
                    else if (part[1] == "Next")
                    {
                        nextLine = currLine;
                    }
                    else if (part[1] == "End")
                    {
                        nextLine = -1;
                    }
                    else
                    {
                        int jumpLine = int.Parse(part[1].Split(':')[1]);
                        nextLine = jumpLine;
                    }

                    if (part[0] == "Player")
                    {
                        if (part[1] == "Choice")
                        {
                            //splitting into 2 choices
                            string[] choice = part[2].Split(';');
                            DialogueLine newLine = new DialogueLine(true, "playerName", "")
                            {
                                options = new string[choice.Length]
                            };

                            for (int i = 0; i < choice.Length; i++)
                                newLine.options[i] = choice[i];

                            lines.Add(newLine);
                        }
                        else
                        {
                            DialogueLine newLine = new DialogueLine(false, "playerName", part[2])
                            {
                                nextLine = nextLine
                            };
                            lines.Add(newLine);
                        }

                    }

                    else
                    {
                        DialogueLine newLine = new DialogueLine(false, part[0], part[2])
                        {
                            nextLine = nextLine
                        };
                        lines.Add(newLine);
                    }
                }
            }
            while (line != null);
            r.Close();
        }
    }

    public bool Check(int lineNumber)
    {
        return lines[lineNumber].isChoice;
    }

    public string GetName(int lineNumber)
    {
        return lines[lineNumber].name;
    }

    public string GetContent(int lineNumber)
    {
        return lines[lineNumber].content;
    }

    public string[] GetOptions(int lineNumber)
    {
        return lines[lineNumber].options;
    }

    public int GetNextLine(int lineNumber)
    {
        return lines[lineNumber].nextLine;
    }
}