<template>
  <vedioList></vedioList>
<!--  <nav>-->
<!--    <router-link to="/page_home">Home</router-link>-->
<!--    <router-link to="/page_1">Page1</router-link>-->
<!--    <router-link to="/page_2">Page2</router-link>-->
<!--  </nav>-->
</template>

<script>
import vedioList from './vedioList'
export default {
  name: 'header',
  components: {vedioList},
  methods: {
    getResource: function () {
      this.$http.get('http://localhost:3306/vedio').then(response => {
        console.log(response.data)
      }, response => {
        console.log('error')
      })
    }
  }
}
</script>

<style scoped>

</style>
