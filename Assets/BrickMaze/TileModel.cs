using UnityEngine;
public class TileModel : MonoBehaviour
{
    [System.Serializable]
    public enum Type
    {
        NONE,
        NORMAL,
        OBJECTIVE1,
        OBJECTIVE2,
        FILLED_OBJECTIVE
    }
    public Type type;

    public Type getType()
    {
        return type;
    }

    public void SetType(Type newType)
    {
        string path = "Materials/";
        switch (newType)
        {
            case Type.NORMAL:
                path += "NormalTile";
                break;
            case Type.OBJECTIVE1:
                path += "ObjectiveTile1";
                break;
            case Type.OBJECTIVE2:
                path += "ObjectiveTile2";
                break;
            case Type.FILLED_OBJECTIVE:
                path += "FilledObjectiveTile";
                break;
            default:
                break;
        }
        Material newMaterial = Resources.Load<Material>(path);
        GetComponent<Renderer>().material = newMaterial;
        type = newType;
    }

}

