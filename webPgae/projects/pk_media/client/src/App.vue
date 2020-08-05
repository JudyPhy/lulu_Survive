<template>
  <div id="app">
    <header v-if="$route.meta.showNav">
      <Menu></Menu>
    </header>
    <div id="content" :style="{height:$route.meta.showNav? 480 + 'px' : 544 + 'px' }">
      <router-view/>
    </div>
    <footer>
      <!--      <img src="https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=366773481,3051478352&fm=26&gp=0.jpg">-->
    </footer>
  </div>
</template>

<script>
import Menu from './components/menu_top'

export default {
  name: 'App',
  components: {Menu}
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
header {
  background-color: #2c3e50;
  overflow: hidden;
  width: 347px;
  height: 64px;
  padding: 0;
  margin: 0;
}
#content {
  background-color: darkgray;
  overflow: hidden;
  width: 347px;
  height: 480px;
  padding: 0;
  margin: 0;
  position: relative;
}
footer {
  background-color: darkolivegreen;
  overflow: hidden;
  width: 347px;
  height: 96px;
  padding: 0;
  margin: 0;
}
</style>
