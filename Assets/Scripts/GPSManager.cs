using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
    public Text text_ui;
    public GameObject welcome_popUp;
    public bool isFirst = false;
    public bool isGold = false;

    public double[] lats;
    public double[] longs;
    public GameObject goldBox_PopUp;
    public Animator goldBox_Anim;
    public GameObject goldBox_container;
    IEnumerator Start()
    {
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start(10, 1);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            while (true)
            {
                yield return null;
                text_ui.text = Input.location.lastData.latitude + " / " + Input.location.lastData.longitude;
            }
        }

        // Input.location.Stop();
    }

    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            double myLat = Input.location.lastData.latitude;
            double myLong = Input.location.lastData.longitude;

            WelcomePopup(myLat, myLong);
            GoldBoxPopup(myLat, myLong);
        }
    }

    // ��ǥ�� �Ÿ� ��� ����(�Ϲ����� ����)
    private double distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;

        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);

        dist = Rad2Deg(dist);

        dist = dist * 60 * 1.1515;

        dist = dist * 1609.344; // ���� ��ȯ

        return dist;
    }
    private void WelcomePopup(double myLat, double myLong)
    {
        double remainDistance = distance(myLat, myLong, lats[0], longs[0]);

            if (remainDistance <= 215f) // 215m
            {
                if (!isFirst)
                {
                    isFirst = true;
                    welcome_popUp.SetActive(true);
                }
            }
    }
    private void GoldBoxPopup(double myLat, double myLong)
    {
        double remainDistance = distance(myLat, myLong, lats[1], longs[1]);

            if (remainDistance <= 10) // 10m
            {
                if (!isGold)
                {
                    isGold = true;
                    goldBox_PopUp.SetActive(true);
                }
            }
    }
    private double Deg2Rad(double deg)
    {
        return (deg * Mathf.PI / 180.0f);
    }

    private double Rad2Deg(double rad)
    {
        return (rad * 180.0f / Mathf.PI);
    }

    public void OnOpenBox()
    {
        goldBox_Anim.SetTrigger("Open");
        Invoke("PopupDelayOff", 2f);
    }
    private void PopupDelayOff()
    {
        goldBox_PopUp.SetActive(false);
        goldBox_container.SetActive(true);
        goldBox_container.transform.position = Camera.main.transform.forward * 3f;
    }
}
