import "./main.js"; 


class Reading {
  constructor(temperature, humidity, id, timestamp){
    this.temperature = temperature;
    this.humidity = humidity;
    this.id = id; 
    const date = new Date(timestamp);
    this.timestamp = date; 
  }
}

const app = Vue.createApp({
    data() {
      return {
        readings: [],
      }
    },
    methods: {
      GetLatest(){
        const response = Axios.get('https://fruitresttest.azurewebsites.net/api/Readings?offset=0&count=1000').then(
          (response) => {
            console.log(response.data); 
            
            response.data.forEach((element) => {
              var toAdd = new Reading(element.temperature, element.humidity, element.id, element.timestamp);
              this.readings.push(toAdd);
            });
          }
        )
        
      }, 

      async POST(){
        const response = await Axios({url: 'https://fruitresttest.azurewebsites.net/api/Readings/nuke', method: 'DELETE'}).then(
          (response) => {
            console.log(response.data); 
            }
          
        );
        
      }

      

    },

    computed: {
        
    },

    mounted() {
      this.GetLatest(); 
      
      this.POST(); 
    }
  })
  
  app.mount('#app');