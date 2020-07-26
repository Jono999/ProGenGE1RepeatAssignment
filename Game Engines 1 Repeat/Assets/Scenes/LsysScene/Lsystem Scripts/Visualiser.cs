using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualiser : MonoBehaviour
{
    public LSystemGenerator lSystem;
    List<Vector3> positions = new List<Vector3>();

   // public RoadHelper roadHelper;

    public RHelpAlt rHelpAlt;

    private int length = 8;
    private float angle = 90f;


    public int Length
    {
        get
        {
            if (length > 0)
            {
                return length;
            }
            else
            {
                return 1;
            }
        }
       
        set => length = value;
    }

    private void Start()
    {
        var sequence = lSystem.GenerateSentence();
        VisualiseSequence(sequence);
    }
    
    private void VisualiseSequence(string sequence)
    {
        Stack<AgentParameters> savePoints = new Stack<AgentParameters>();
        var currentPosition = Vector3.zero;
        
        Vector3 direction = Vector3.forward;
        Vector3 tempPosition = Vector3.zero;
        
        positions.Add(currentPosition);

        foreach (var letter in sequence)
        {
            SimpleVisualiser.EncodingLetters encoding = (SimpleVisualiser.EncodingLetters) letter;

            switch (encoding)
            {
                case SimpleVisualiser.EncodingLetters.save:
                    savePoints.Push(new AgentParameters
                    {
                        position = currentPosition,
                        direction = direction,
                        length = Length
                    });
                    break;
                case SimpleVisualiser.EncodingLetters.load:
                    if (savePoints.Count > 0)
                    {
                        var agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.position;
                        direction = agentParameter.direction;
                        Length = agentParameter.length;
                    }
                    else
                    {
                        throw new System.Exception("Don't have save point in our stack");
                    }
                    break;
                case SimpleVisualiser.EncodingLetters.draw:
                    tempPosition = currentPosition;
                    currentPosition += direction * length;
                   // roadHelper.PlaceStreetAtPositions(tempPosition, Vector3Int.RoundToInt(direction), length);
                    rHelpAlt.PlaceStreetAtPositions(tempPosition, Vector3Int.RoundToInt(direction), length);
                    //DrawLine(tempPosition, currentPosition, Color.yellow);
                    Length -= 2;
                    positions.Add(currentPosition);
                    break;
                case SimpleVisualiser.EncodingLetters.turnRight:
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case SimpleVisualiser.EncodingLetters.turnLeft:
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }
        }
        //roadHelper.FixRoad();
        rHelpAlt.FixRoad();
    }
}
