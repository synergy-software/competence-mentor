var apiRoot = "http://localhost:2392/api/";
var userId = 55;
Vue.component('aggregations', {
  template:'#Aggregations',
    mounted: function(){
    var diameter = 500; //max size of the bubbles
    var color    = d3.scale.category20c(); //color category

    var bubble = d3.layout.pack()
        .sort(null)
        .size([diameter, diameter])
        .value(function(d){return d.size})
        .padding(1.5);

    var svg = d3.select("#Graph")        
        .attr("width", diameter)
        .attr("height", diameter)
        .attr("class", "bubble");
   

    //bubbles needs very specific format, convert data to this.
    var nodes = bubble.nodes({children:[
      {name:"java", size:5},
      {name:"js", size:10},
      {name:"c#", size:15},
      {name:"elastic", size:20},
    ]}).filter(function(d) { return !d.children; });

    //setup the chart
    var bubbles = svg.append("g")
        .attr("transform", "translate(0,0)")
        .selectAll(".bubble")
        .data(nodes)
        .enter();

    //create the bubbles
    bubbles.append("circle")
        .attr("r", function(d){ return d.r; })
        .attr("cx", function(d){ return d.x; })
        .attr("cy", function(d){ return d.y; })
        .style("fill", function(d) { return color(d.value); });

    //format the text for each bubble
    bubbles.append("text")
        .attr("x", function(d){ return d.x; })
        .attr("y", function(d){ return d.y + 5; })
        .attr("text-anchor", "middle")
        .text(function(d){ return d.name; })
        .style({
            "fill":"white", 
            "font-family":"Helvetica Neue, Helvetica, Arial, san-serif",
            "font-size": "12px"
        });
  }, 
});

Vue.component('search', {
  template:'#Search',
  data:function(){
   
    return {
      results:[
       
      ],
      term:"",
      showNoResults:false     
    };
  },
  methods:{
    search:function(){
      var that = this;
      $.ajax({
        url: apiRoot+"competence/search?compentence="+this.term,
        method:"GET",
        contentType:'application/json',
        
      }).done(function(result){
        that.results.length = 0;
        that.showNoResults = result.length == 0;
        result.forEach(function(item){
          that.results.push({name:item})
        });
      });
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
      });
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