import axios from 'axios'; 
import {BarController,
  BarElement,
  CategoryScale,
  Chart,
  Legend,
  LineController,
  LineElement,
  LinearScale,
  PointElement,
  TimeScale,
  Title,
  Tooltip,
  _adapters,
  _detectPlatform
  } from 'chart.js' 


Chart.register(BarController, BarElement, Legend, LinearScale, LineElement, LineController,CategoryScale, PointElement, Title, Tooltip, TimeScale);

import 'chartjs-adapter-date-fns'



class Reading {
  constructor(temperature, humidity, id, timestamp) {
    this.temperature = temperature;
    this.humidity = humidity;
    this.id = id;
    const date = new Date(timestamp);
    this.timestamp = date;
  }
}

class Recommendation {
  constructor(title, link) {
    this.title = title;
    this.link = link;
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

const chartNumberOne = document.getElementById('tempChart')

let temperatureChart = new Chart(chartNumberOne, {
  type: 'line',
  data: {
    datasets: [{
      label: 'Temperatur over tid',
      data: [],
      borderWidth: 2,
      borderColor: getComputedStyle(document.documentElement).getPropertyValue('--accent'),
      backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--secondaryaccent'),
      pointStyle: 'circle',
      pointRadius: 7,
      pointHoverRadius: 10,
      pointBorderColor: getComputedStyle(document.documentElement).getPropertyValue('--accent')

    }],

  },
  options: {
    scales: {
      x: {
        type: 'time',
        time: {
          unit: 'hour', 
          tooltipFormat: 'dd/MM/yyyy HH:mm',
          displayFormats: {
            hour: 'HH:mm', 
            minute: 'HH:mm:ss', 
            
          }
        },
        title: {
          display: true,
          text: 'Tidsinterval for målinger ',          
          color: getComputedStyle(document.documentElement).getPropertyValue('--chartHeadline'),
          font: {
            size: 12,
            weight: 'bold',
            family: "Montserrat Alternates",
        }
        },
        ticks: {
          source: 'data',
          autoSkip: true,
          maxTicksLimit: 5,
        }
        
      },
      y: {
        title: {
          display: true,
          text: 'Temperatur',
          color: getComputedStyle(document.documentElement).getPropertyValue('--chartHeadline'),
          font: {
            size: 12,
            weight: 'bold',
            family: "Montserrat Alternates",
        }
        }
      }
    },
    layout: {
      padding: {
          right: 25
          
      }
    },
    responsive: true,
    maintainAspectRatio: true,
    plugins: {
      legend: {
        labels: {
          boxWidth:10,
            // This more specific font property overrides the global property
            font: {
                size: 18,
                weight: 'bold',
                family: "Montserrat Alternates",
                color: 'black'
            }
        }
    }
    },
  }
});

const app = Vue.createApp({
  data() {
    return {
      readings: [],
      foods: [],
      recommendedRecipes: [],
      baseURL: "",
      ReadingBaseURL: "" ,
      FoodsBaseURL: "",
      newestTemperature: NaN,
      newestHumidity: NaN,
      fruitCheck: true,
      vegetableCheck: true,
      spoilTime: 5,
      chosenFood: undefined,
      chosenFoodString: "",
      searchFoodString: "",
      isFetchingMealDB: false

    }
  },
  methods: {
    async GetLatest() {
      const response = await axios.get(this.readingBaseURL + '?offset=0&count=5').then(
        (response) => {

          response.data.forEach((element) => {
            var toAdd = new Reading(element.temperature, element.humidity, element.id, element.timestamp);
            this.readings.push(toAdd);

          },
          );
          this.updateChartings();
        }
      );

    },


    async GetFoods() {

      let baseURL = this.foodsBaseURL+"/filtered/";
      if (!this.fruitCheck && !this.vegetableCheck);
      else {


        baseURL += `?filterFruit=${this.fruitCheck}&filterVegetable=${this.vegetableCheck}`
        console.log(baseURL);
      }

      console.log(baseURL);


      const response = await axios.get(baseURL).then(
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



    async GetFoodsByName() {

      let baseURL = this.foodsBaseURL;
      if (!this.fruitCheck && !this.vegetableCheck) {
        baseURL += `/filtered/?filterFruit=true&filterVegetable=true&filterName=${this.chosenFoodString}&offset=0&count=5`
        console.log(baseURL);
      }
      else {

        baseURL += `/filtered/?filterFruit=${this.fruitCheck}&filterVegetable=${this.vegetableCheck}&filterName=${this.chosenFoodString}&offset=0&count=5`
        console.log(baseURL);
      }

      console.log(baseURL);


      const response = await axios.get(baseURL).then(
        (response) => {
          var respData = response.data;

          var foundFoods = [];
          if (response.data == null || response.data == "") {
            this.foods = []
            return
          }
          response.data.forEach((element) => {
            const currentFood = new Food(element.foodTypeId, element.foodTypeName, element.id, element.name, element.apiLink, element.spoilDate, element.spoilHours, element.idealTemperature, element.idealHumidity);

            foundFoods.push(currentFood);
          });
          
          this.foods = foundFoods
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
      // foodname argument is assumed to be all lowercase

      const map = {
        "agurk": this.CalculateGenericFood(3, 2, 8),
        "banan": this.CalculateGenericFood(5, 1, 3),
        "hvidløg": this.CalculateGenericFood(12, 130, 7),
        "kartoffel": this.CalculateGenericFood(15, 3, 4),
        "æble": this.CalculateGenericFood(10, 1, 3),

      }
      console.log(map[foodName]);
      return map[foodName];
    },




    CalculateGenericFood(range, penaltydays, penaltyhours) {
      console.log("Hour:" + penaltyhours);
      console.log("day:" + penaltydays);

      const food = this.chosenFood;

      const isIdeal = (this.newestHumidity >= food.idealhumidity - range && this.newestHumidity <= food.idealHumidity + range && this.newestTemperature >= food.idealTemperature - range && this.newestTemperature <= food.idealTemperature + range);
      if (isIdeal) {
        console.log(food.spoildays, food.spoilhours)
        return [food.spoildays, food.spoilhours];
      }

      if (food.spoilhours - penaltyhours < 0) {
        penaltyhours -= food.spoilhours;
        let durabilityHours = 24 - penaltyhours;
        let durabiliyDays = food.spoildays - (penaltydays + 1);

        return [durabiliyDays, durabilityHours];
      }

      let durabilityHours = food.spoilhours - penaltyhours;
      let durabiliyDays = food.spoildays - penaltydays;

      return [durabiliyDays, durabilityHours];
    },

    async ChooseFruit() {
      this.chosenFood = this.foods.find((elem) => elem.name.toLowerCase() == this.chosenFoodString.toLowerCase());
      if (this.chosenFood == null || this.chosenFood == "") {
        this.recommendedRecipes = [];
        return
      }
      this.chosenFoodImage = `https://themealdb.com/images/ingredients/${this.chosenFood.apilink}.png`;
      console.log(this.chosenFoodImage);  
      console.log(Vue.toRaw(this.chosenFood));
      const food = this.chosenFood;
      this.spoilTime = this.spoilMap(food.spoilhours, food.spoildays, food.name.toLowerCase());
      await this.FetchMealDB(); 
    },

    HandleChooseFood(e){
      if(e.key == "Enter"){
        this.ChooseFruit()
        console.log(this.chosenFoodImage)
      }
      else if (e.keyCode == "114"){
        console.log("shit fam");
      }
    },

    updateChartings() {
      const chartData = this.readings.map(reading => ({
        x: reading.timestamp.toISOString(),
        y: reading.temperature,
        
      }));
      console.log()
      temperatureChart.data.datasets[0].data = chartData;
      temperatureChart.update();
      

    },

    async FetchMealDB() {
      this.recommendedRecipes = []; 
      if (this.isFetchingMealDB) {
        return
      }
      this.isFetchingMealDB = true
      const baseAPIlink = "www.themealdb.com";
      
      console.log("https://" + baseAPIlink + `/api/json/v1/1/filter.php?i=${this.chosenFood.apilink}`);
      const response = await axios.get("https://" + baseAPIlink + `/api/json/v1/1/filter.php?i=${this.chosenFood.apilink}`).then(
        (response) => {
         
          response.data.meals.every((elem) => {
            if (this.recommendedRecipes.length == 3){
              return false; 
            }
            const newRecommendation = new Recommendation(elem.strMeal, elem.strMealThumb);
            
            this.recommendedRecipes.push(newRecommendation);
            return true; 
          });
          console.log(this.recommendedRecipes);
        }
      ).catch(function(error) {


      });

      this.isFetchingMealDB = false

    },
  },







  computed: {

  },

  mounted() {
    this.baseURL = import.meta.env.VITE_BASE_URL
    this.readingBaseURL = this.baseURL + "api/Readings";
    this.foodsBaseURL = this.baseURL +"api/Foods"
    this.SetupInitialData();
    this.GetFoodsByName();
    



    //this.POST(); 
  }
})

app.mount('#app');