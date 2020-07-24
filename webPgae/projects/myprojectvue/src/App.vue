<template>
  <div id="app">
    <header>
      <VedioNav></VedioNav>
    </header>
    <router-view/>
  </div>
</template>

<script>
import VedioNav from './components/vedioNav'

export default {
  name: 'App',
  components: {VedioNav}
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

a.router-link-active {color: red;}
</style>
