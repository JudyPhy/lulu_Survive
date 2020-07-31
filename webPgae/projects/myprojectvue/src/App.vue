<template>
  <div id="app">
    <PageInit></PageInit>
  </div>
</template>

<script>
import PageInit from './components/page_base'

export default {
  name: 'App',
  components: {PageInit}
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  padding: 0;
  margin: 0;
  background-color: burlywood;
}

a.router-link-active {color: red;}
</style>
