using UnityEngine;
using UnityEngine.UI;

public class EffectsScene : MonoBehaviour
{
    public static GameObject[] m_destroyObjects = new GameObject[30];
    public static int inputLocation;

    public Transform[] m_effects;
    public Text m_effectName;
    private int index;

    private void Awake()
    {
        inputLocation = 0;
        m_effectName.text = m_effects[index].name;
        MakeObject();
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (index <= 0)
                index = m_effects.Length - 1;
            else
                index--;

            MakeObject();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (index >= m_effects.Length - 1)
                index = 0;
            else
                index++;

            MakeObject();
        }

        if (Input.GetKeyDown(KeyCode.C))
            MakeObject();
    }

    private void MakeObject()
    {
        DestroyGameObject();
        var gm = Instantiate(m_effects[index],
            m_effects[index].transform.position,
            m_effects[index].transform.rotation).gameObject;
        m_effectName.text = index + 1 + " : " + m_effects[index].name;
        gm.transform.parent = transform;
        m_destroyObjects[inputLocation] = gm;

        inputLocation++;
    }

    private void DestroyGameObject()
    {
        for (var i = 0; i < inputLocation; i++) Destroy(m_destroyObjects[i]);
        inputLocation = 0;
    }
}