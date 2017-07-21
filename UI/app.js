var apiRoot = "http://localhost:2392/api/";
var userId = 55;
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
     this.loadData();
    return {
      message: '',
      competences: []
    };
  },
  methods: {
    update: function () {
      var that = this;
      $.ajax({
        url: apiRoot+"competence",
        method:"POST",
        contentType:'application/json',
        dataType: "json",
        data:JSON.stringify({
          UserId:userId,
          CompetenceText: this.message
        })
      }).done(function(){
        that.loadData();
      })
    },
    loadProfile: function () {

    },
    loadSearch: function () {

    },
    loadAggregations: function () {

    },
    loadData(){
      var that = this;
      $.get(apiRoot+"Competence/"+userId).done(function(data){
          that.message = data.CompetenceText;
          that.competences.length = 0;
          data.Competencies.forEach(function(el){
            that.competences.push({text:el});
          });
      });
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