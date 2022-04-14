
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class CPUStats : MonoBehaviour
{
    public Text _text;
    PerformanceCounter cpuCounter;
    System.Diagnostics.PerformanceCounter ramCounter;
    // Start is called before the first frame update
    void Start()
    {
        System.Diagnostics.PerformanceCounterCategory.Exists("PerformanceCounter");
         
         cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
     
         cpuCounter.CategoryName = "Processor";
         cpuCounter.CounterName = "% Processor Time";
         cpuCounter.InstanceName = "_Total";
     
         ramCounter = new PerformanceCounter("Memory", "Available MBytes");
     }
    

    // Update is called once per frame
    void Update()
    {   
        _text.text = "> cpu: "+getCurrentCpuUsage()+"; >ram: "+getAvailableRAM();

        // Debug.Log("> cpu: "+getCurrentCpuUsage()+"; >ram: "+getAvailableRAM());
         
    }

    public string getCurrentCpuUsage(){
                 return cpuCounter.NextValue()+"%";
     }
 
     public string getAvailableRAM(){
                 return ramCounter.NextValue()+"MB";
     } 

}
