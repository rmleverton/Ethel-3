using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    // [SerializeField] private string name;
    [SerializeField] private string cat_type;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int[] location;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Create(Cat_Struct cat_struct){
        cat_type = cat_struct.cat_type;
        sprites = cat_struct.sprites;
        location = new int[]{0,1};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] Get_Location(){
        return location;
    }

    public void Set_Location(int[] new_loc){
        location = new_loc;
        //Debug.Log("Location: " + location[0] + "," + location[1]);
    }

    public Sprite Get_Sprite(int[] sprite_loc){
        //0-0, 1-3, 4-6, 7-9
        int sprite_num = 0;

        if(sprite_loc[0] == 0){
            sprite_num = 0;
        }
        else if(sprite_loc[0] == 1){
            if(sprite_loc[1] == 1){
                sprite_num = 1;
            }
            else if(sprite_loc[1] == 2){
                sprite_num = 2;
            }
            else if(sprite_loc[1] == 3){
                sprite_num = 3;
            }

        }
        else if(sprite_loc[0] == 2){
            if(sprite_loc[1] == 1){
                sprite_num = 4;
            }
            else if(sprite_loc[1] == 2){
                sprite_num = 5;
            }
            else if(sprite_loc[1] == 3){
                sprite_num = 6;
            }
        }
        else if(sprite_loc[0] == 3){
            if(sprite_loc[1] == 1){
                sprite_num = 7;
            }
            else if(sprite_loc[1] == 2){
                sprite_num = 8;
            }
            else if(sprite_loc[1] == 3){
                sprite_num = 9;
            }
        }
        return sprites[sprite_num];
    }
}
