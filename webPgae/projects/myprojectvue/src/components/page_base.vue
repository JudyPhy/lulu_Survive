<template>
  <div class="page_base">
    <header>
      <Menu></Menu>
    </header>
    <div id="content" @touchstart="touchstart">
      <ContentContainer></ContentContainer>
    </div>
    <footer>
<!--      <img src="https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=366773481,3051478352&fm=26&gp=0.jpg">-->
    </footer>
  </div>
</template>

<script>
import Menu from './menu_top'
import ContentContainer from './container_dropdown'
export default {
  name: 'page_base',
  components: {Menu, ContentContainer},
  data: function () {
    return {
    }
  },
  methods: {
    touchstart: function () {
      console.log('111 touchstart')
    }
  }
}
</script>

<style scoped>
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
