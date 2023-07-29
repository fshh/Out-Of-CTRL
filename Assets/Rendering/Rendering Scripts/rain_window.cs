﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rain_window : MonoBehaviour
{
    public static int imgWidth = 64;
    public static int imgHeight = 64;
    public Color[] colorArray = new Color[imgWidth * imgHeight];
    public Color initialColor = new Color (0f, 0f, 0f, 0f);
    // Chance of a new droplet per frame
    public float newDropChance = 0.1f;
    // to deal with variable framerate
    // minimum time for raindrop update, in seconds
    public float dripClockMin = 0.1f;
    // time accumilated since last raindrop update
    private float accumulatedTime = 0f;
    
    Texture2D texture;
    
    // Start is called before the first frame update
    void Start()
    {
        // create texture
        texture = new Texture2D(imgWidth, imgHeight);
        // Make pixelated?
        texture.filterMode = FilterMode.Point;
        texture.requestedMipmapLevel = 0;
        // initialize array to initialColor
        for (int i = 0; i < colorArray.Length; i++){
            colorArray[i] = initialColor;
        }
        // Make to intial color
        texture.SetPixels(colorArray);
        
        // Rendering/attaching to object stuffs
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
        texture.Apply();
        renderer.material.SetTexture("_MainTex", texture);
    }

    // Update is called once per frame
    void Update()
    {
        // Controlling the speed of rain
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > dripClockMin) {
            // only runs after accumilated time meets clock
            accumulatedTime = 0f;
            // New raindrops
            if (Random.Range(0f, 1f) < newDropChance){
                float dropSize = Random.Range(0.2f, 1f);
                int position = Random.Range(0, colorArray.Length);
                colorArray[position] =  colorArray[position] + new Color (dropSize, dropSize, dropSize, dropSize);
            }
            
            // Raindrop flow
            // for (int i = colorArray.Length - 1; i >= 0; i--) { // if want to update from top to bottom
            for (int i = 0; i < colorArray.Length; i++) {
                // current selected drop size
                float selectDrop = colorArray[i].a;
                if (selectDrop > 1f) {
                    selectDrop = 1;
                }
                // handling water tension/droplets combining
                // ***WIP***
                
                // handling gravity
                // determine if drop will move
                if (selectDrop > 0.85f && selectDrop > Random.Range(0f, 1.2f)) {
                    // drop will move
                    if (i >= imgWidth) {
                        // drop not at bottom row
                        colorArray[i- imgWidth] = colorArray[i- imgWidth] + colorArray[i];
                    }
                    colorArray[i] = initialColor;
                }
                // reset any oversized raindrops
                if (colorArray[i].a > 1f) {
                    colorArray[i] = new Color(1f, 1f, 1f, 1f);
                }
            }
            
            // Update above
            texture.SetPixels(colorArray);
            texture.Apply();
            GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

        }
    }
}
