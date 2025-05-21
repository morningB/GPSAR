using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;
    void OnEnable() => manager.trackablesChanged.AddListener(OnChanged);
    

    void OnDisable() => manager.trackablesChanged.RemoveListener(OnChanged);

    public List<GameObject> list1 = new List<GameObject>();
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();

    public List<AudioClip> list2 = new List<AudioClip>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();
    void Start()
    {
        foreach (GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }
        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    // void OnEnable()
    // {
    //     manager.trackedImagesChanged += OnChanged;

    // }
    // void OnDisable()
    // {
    //     manager.trackedImagesChanged -= OnChanged;
    // }
    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var t in eventArgs.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }
        foreach (var t in eventArgs.updated)
        {
            UpdateImage(t);
        }
        
    }
    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        GameObject o = dict1[name];
        o.transform.position = t.transform.position;
        o.transform.rotation = t.transform.rotation;
        o.SetActive(true);
    }
    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        AudioClip sound = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
