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
  el: '#app'

})