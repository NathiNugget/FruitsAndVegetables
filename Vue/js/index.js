import "./bundle.js";

const ReadingBaseURL = "https://fruitresttest.azurewebsites.net/api/Readings";
const FoodsBaseURL = "https://fruitresttest.azurewebsites.net/api/Foods"

class Reading {
  constructor(temperature, humidity, id, timestamp) {
    this.temperature = temperature;
    this.humidity = humidity;
    this.id = id;
    const date = new Date(timestamp);
    this.timestamp = date;
  }
}

class Food {
  constructor(foodTypeId, foodTypeName, id, name, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity) {
    this.foodtypeid = foodTypeId;
    this.foodtypename = foodTypeName;
    this.id = id;
    this.name = name;
    this.apilink = apiLink;
    this.spoildays = spoilDate;
    this.spoilhours = spoilHours;
    this.idealtemp = idealTemperature;
    this.idealhumidity = idealHumidity;
  }



}

const app = Vue.createApp({
  data() {
    return {
      readings: [],
      foods: [],
      newestTemperature: NaN,
      newestHumidity: NaN,
      fruitCheck: false,
      vegetableCheck: false,
      spoilTime: 5,
      chosenFood: undefined,
      chosenFoodString: undefined,

    }
  },
  methods: {
    async GetLatest() {
      const response = await Axios.get(ReadingBaseURL + '?offset=0&count=5').then(
        (response) => {

          response.data.forEach((element) => {
            var toAdd = new Reading(element.temperature, element.humidity, element.id, element.timestamp);
            this.readings.push(toAdd);

          }

          );

        }
      );

    },


    async GetFoods() {

      let baseURL = FoodsBaseURL;
      if (!this.fruitCheck && !this.vegetableCheck);
      else {


        baseURL += `?filterFruit=${this.fruitCheck}&filterVegetable=${this.vegetableCheck}`
        console.log(baseURL);
      }

      console.log(baseURL);


      const response = await Axios.get(baseURL).then(
        (response) => {
          var respData = response.data;

          this.foods = [];
          response.data.forEach((element) => {
            const currentFood = new Food(element.foodTypeId, element.foodTypeName, element.id, element.name, element.apiLink, element.spoilDate, element.spoilHours, element.idealTemperature, element.idealHumidity);

            this.foods.push(currentFood);
          });
          console.log(this.foods);

        }
      );

    },



    async SetupInitialData() {

      await this.GetLatest();
      const readingObj = Vue.toRaw(this.readings[0]);
      this.newestHumidity = readingObj.humidity;
      this.newestTemperature = readingObj.temperature;
    },

    spoilMap(hour, day, foodName) {


      const map = {
        "Agurk": this.CalculateGenericFood(3, 2, 8),
        "Banan": this.CalculateGenericFood(5, 1, 3),

        "Kartoffel": this.CalculateGenericFood(15, 3, 4),
        "Æble": this.CalculateGenericFood(10, 1, 3),

      }
      console.log(map[foodName]);
      return map[foodName];
    },




    CalculateGenericFood(range, penaltydays, penaltyhours) {
      console.log("Hour:" + penaltyhours);
      console.log("day:" + penaltydays);

      const fruit = this.chosenFood;

      const isIdeal = (this.newestHumidity >= fruit.idealhumidity - range && this.newestHumidity <= fruit.idealHumidity + range && this.newestTemperature >= fruit.idealTemperature - range && this.newestTemperature <= fruit.idealTemperature + range);
      if (isIdeal) {
        console.log(fruit.spoildays, fruit.spoilhours)
        return [fruit.spoildays, fruit.spoilhours];
      }

      if (fruit.spoilhours - penaltyhours < 0) {
        penaltyhours -= fruits.spoilhours;
        let durabilityHours = 24 - penaltyhours;
        let durabiliyDays = fruit.spoildays - (penaltydays + 1);

        return [durabiliyDays, durabilityHours];
      }

      let durabilityHours = fruit.spoilhours - penaltyhours;
      let durabiliyDays = fruit.spoildays - penaltydays;

      return [durabiliyDays, durabilityHours];
    },

    ChooseFruit() {
      this.chosenFood = this.foods.find((elem) => elem.name == this.chosenFoodString);
      console.log(Vue.toRaw(this.chosenFood));
      const food = this.chosenFood;
      this.spoilTime = this.spoilMap(food.spoilhours, food.spoildays, food.name);
    },




  },






  computed: {

  },

  mounted() {
    this.SetupInitialData();
    this.GetFoods();



    //this.POST(); 
  }
})

app.mount('#app');