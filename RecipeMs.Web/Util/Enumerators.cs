using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeMs.Web.Util
{
    public class Enumerators
    {
       public enum FoodGroup
        {
            [Display(Name= "Dairy and Egg")]
            DairyAndEgg =1,

            [Display(Name= "Spice and Herb")]
            SpiceAndHerb,

            [Display(Name = "Fat and Oil")]
            FatAndOil,

            Poultry,

            [Display(Name = "Soup, Sauce and Gravy")]
            SoupSauceAndGravy,

            [Display(Name = "Sausage and Luncheon Meat")]
            SausageAndLuncheonMeat,
            Fruit,
            Pork,
            Vegetable,

            [Display(Name= "Nut and Seed")]
            NutAndSeed,

            Beef,
            Beverage,
            Fish,
            Legume,

            [Display(Name= "Lamb, Veal and Game meat")]
            LambVealAndGame,
            Baked,
            Sweet,

            [Display(Name= "Cereal Grains and Pasta")]
            CerealGrainsAndPasta
        }
    }
}