<template>
  <div class="page_base" :style="{width:this.screenWidth + 'px', height:this.screenHeight + 'px'}">
    <header>
      <Menu :screenWidth=this.menuWidth :screenHeight=this.menuHeight></Menu>
    </header>
    <div class="posterList">
      <router-view/>
    </div>
    <div class="adv">
      <img src="https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=366773481,3051478352&fm=26&gp=0.jpg">
    </div>
  </div>
</template>

<script>
import Menu from './menu_top'
export default {
  name: 'page_base',
  components: {Menu},
  data: function () {
    return {
      screenWidth: 0,
      screenHeight: 0,
      menuWidth: 0,
      menuHeight: 0
    }
  },
  mounted () {
    this.screenWidth = document.documentElement.clientWidth
    this.screenHeight = document.documentElement.clientHeight
    this.menuWidth = this.screenWidth
    this.menuHeight = this.screenHeight / 15
    window.onresize = () => {
      return (() => {
        this.screenWidth = document.documentElement.clientWidth
        this.screenHeight = document.documentElement.clientHeight
        this.menuWidth = this.screenWidth
        this.menuHeight = this.screenHeight / 15
      })()
    }
  }
}
</script>

<style scoped>
.posterList {
  background-color: aquamarine;
  overflow: hidden;
  width: 100%;
  height: 80%;
}
.adv img {
  max-width: 100%;
  max-height: 100%;
}
</style>
