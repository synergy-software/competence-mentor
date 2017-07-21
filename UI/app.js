Vue.component('aggregations', {
  template:'#Aggregations'
});

Vue.component('search', {
  template:'#Search',
  data:function(){
    return {
    results:[
      {name:"cepi"},
      {name:"mace"},
      {name:"wosw"}
    ]
  };
  },
  methods:{
    search:function(){
      this.results.push({name:"xxxx"});
    }
  }
});
Vue.component('profile', {

  template: '#Profile',

  props: [],
  data: function () {
    return {
      message: 'Moje ulubione technologie to #c# #java oraz #elasticsearch',
      competences: [{
        id: "ss",
        text: "c#"
      }, {
        id: "xx",
        text: "Java"
      }, {
        id: "aa",
        text: "elasticsearch"
      }]
    };
  },
  methods: {
    update: function () {
      alert(this.message);
    },
    loadProfile: function () {

    },
    loadSearch: function () {

    },
    loadAggregations: function () {

    }
  }
});


var app = new Vue({
  el: '#app',
  data: {    
   isAggregations:false,
   isSearch:false,
   isProfile:true,
   currentPage: "profile"
  },
  methods:{
    showProfile:function(){
      this.currentPage = "profile";
      this.isProfile = true;
      this.isSearch = false;
      this.isAggregations = false;
    },
    showSearch:function(){
      this.currentPage = "search";     
    },
    showAggregations:function(){
       this.currentPage = "aggregations";   
    }
  }
})