using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public void PlaneType(int index)
    {
        SaveSelectedValue(index, "plane-type");
    }
    public void PlaneMovement(int index)
    {
        SaveSelectedValue(index, "plane-movement");
    }

    private void SaveSelectedValue(int index, string key)
    {
        switch (index)
        {
            case 0: PlayerPrefs.SetInt(key, 0); break; // fighter atau circular
            case 1: PlayerPrefs.SetInt(key, 1); break; // helicopter atau kiri - kanan
            case 2: PlayerPrefs.SetInt(key, 2); break; // bombardier atau kanan - kiri
            case 3: PlayerPrefs.SetInt(key, 3); break; // depan - belakang
        }

        // cek sudah tersimpan belum
        int val = PlayerPrefs.GetInt(key);
        Debug.Log(key + ": " + val);
    }
}
