import "./bundle.js";


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
  constructor(id, name, isvegetable, apilink, spoildays, spoilhours, idealtemp, idealhumidity) {
    this.id = id;
    this.name = name;
    this.isvegetable = isvegetable;
    this.apilink = apilink;
    this.spoildays = spoildays;
    this.spoilhours = spoilhours;
    this.idealtemp = idealtemp;
    this.idealhumidity = idealhumidity;
  }
}

const app = Vue.createApp({
  data() {
    return {
      readings: [],
      newestTemperature: NaN,
      newestHumidity: NaN,
      spoilTime: NaN,
      chosenFood: undefined,

    }
  },
  methods: {
    async GetLatest() {
      const response = await Axios.get('https://fruitresttest.azurewebsites.net/api/Readings?offset=0&count=5').then(
        (response) => {
          console.log(response.data);

          response.data.forEach((element) => {
            var toAdd = new Reading(element.temperature, element.humidity, element.id, element.timestamp);
            this.readings.push(toAdd);

          }

          );
          
        }
      );

    },

    async POST() {
      const response = await Axios({ url: 'https://fruitresttest.azurewebsites.net/api/Readings/nuke', method: 'options' }).then(
        (response) => {
          console.log(response.data);
        }

      );

    },

    async GetFood() {
      const response = await Axios.get(`https://fruitresttest.azurewebsites.net/api/Foods/GetByName?name=${chosenFood}`).then(
        (response) => {
          console.log(response.data);
          var respData = response.data;
          const currentFood = new Food(respData.id, respData.name, respData.isvegetable, respData.apilink, respData.spoildays, respData.spoilhours, respData.idealtemp, respData.idealhumidity);
          this.chosenFood = currentFood;
        }
      );

    },

    async SetupInitialData() {
      await this.GetLatest(); 
      const readingObj = Vue.toRaw(this.readings[0]); 
      this.newestHumidity = readingObj.humidity; 
      this.newestTemperature = readingObj.temperature; 
    },


  },






  computed: {

  },

  mounted() {
    this.SetupInitialData();




    //this.POST(); 
  }
})

app.mount('#app');