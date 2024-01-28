using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [SerializeField] private GameManager game_manager;
    [SerializeField] private List<Cat> cats;
    [SerializeField] private GameObject cat_prefab;

    // private string[] cat_types = {"bernie", "jb", "mog", "peach", "pickle", "selda", "shmuck", "toast", "tofu"};
    // public Sprite[,] cat_sprites; //A. cat type, B. Sprites
    // Start is called before the first frame update

    public Cat_Struct[] cat_structs;
    [SerializeField] private SpriteRenderer[] away, alley, roof, street;
    private SpriteRenderer[][] locations_array;


    void Start()
    {
        locations_array = new SpriteRenderer[][]{away, alley, roof, street};

        for(int i = 0; i < locations_array.Length; i++){
            SpriteRenderer[] loc = locations_array[i];
            for(int j = 0; j < loc.Length; j++){
                SpriteRenderer renderer = loc[j];
                // renderer.enabled = false;
                renderer.sprite = null;
            }
        }

        cats = new List<Cat>();
        //Spawn_Cat();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.frameCount % 100 == 0){
        //     foreach(Cat cat in cats){
        //         int rand = Random.Range(-1, 2);
        //         Debug.Log("Direction: " + rand);
        //         Move_Cat(cat, rand);
        //     }
        // }
    }

    public void Move_Cats_Random(){
        foreach(Cat cat in cats){
            int rand = Random.Range(-1, 2);
            //Debug.Log("Direction: " + rand);
            Move_Cat(cat, rand);
        }
    }

    public void Spawn_Cat(){
        int picker = Random.Range(0,9);
        
        GameObject new_cat_object = Instantiate(cat_prefab);
        new_cat_object.transform.SetParent(transform);
        Cat new_cat_script = new_cat_object.GetComponent<Cat>();
        new_cat_script.Create(cat_structs[picker]);
        cats.Add(new_cat_script);
    }

    public void Move_Cat(Cat cat, int dir){
        int[] cur_loc = cat.Get_Location();
        
        int level = cur_loc[1];
        int place = cur_loc[0];

        int new_level = level + dir;
        int new_place = place;

        if(place == 0){
            if(dir != 0){
                new_place = Pick_Loc();
                new_level = 1;
            }
            else{
                new_place = 0;
                new_level = 1;
            }
        }
        else{
            if(new_level <= 0){
                new_place = 0;
                new_level = 1;
            }
            if(new_level >= 4){
                new_level = 3;
            }
        }


        int[] new_loc = new int[]{new_place, new_level};
        // check if occupied
        cat.Set_Location(new_loc);
        
        //print("Current Location:" + cur_loc[0] + "," + cur_loc[1]);
        print("New Location:" + new_loc[0] + "," + new_loc[1]);
        //print(locations_array[place][level-1].sprite);

        locations_array[place][level-1].sprite = null;
        locations_array[new_place][new_level-1].sprite = cat.Get_Sprite(new_loc);

        game_manager.cat_place = place;

    }

    private int Pick_Loc(){
        int rand = Random.Range(0,4);
        return rand;
    }

    public List<Cat> Get_Cats(){
        return cats;
    }

    public Cat Get_Cat(int id){
        return cats[id];
    }

    public void Remove_Cat(Cat cat){
        int[] cat_loc = cat.Get_Location();
        locations_array[cat_loc[0]][cat_loc[1]-1].sprite = null;
        cats.Remove(cat);
    }
}
[System.Serializable]
public struct Cat_Struct{
    public string cat_type;
    public Sprite[] sprites;
    // public Sprite alley1, alley2, alley3, roof1, roof2, roof3, street1, street2, street3;
}
