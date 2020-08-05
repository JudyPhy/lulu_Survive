<template>
  <div class="container">
    <div v-for="(item,index) in mediaInfoArray" :key="index" @click="selectedItem(item.url)">
      <img :src="item.img">
      <br>{{item.name| ellipsis}}
    </div>
  </div>
</template>

<script>
export default {
  name: 'posterGridList',
  props: {
    mediaInfoArray: Array
  },
  data: function () {
    return {
    }
  },
  filters: {
    ellipsis (value) {
      if (!value) return ''
      if (value.length > 20) {
        return value.slice(0, 20) + '...'
      }
      return value
    }
  },
  methods: {
    selectedItem: function (url) {
      console.log('to:', url)
      // let routerUrl = this.$router.resolve({
      //   path: '/singleVedio',
      //   query: {id: 1}
      // })
      // window.open(routerUrl.href, '_blank')
    }
  }
}
</script>

<style>
.container{
  background-color: blueviolet;
  width: 347px;
  /*height: 480px;*/
  display: grid;
  grid-template-columns: repeat(3, 100px);
  grid-template-rows: repeat(3, 140px);
  grid-column-gap: 10px;
  grid-row-gap: 10px;
  font-size: 16px;
  word-wrap: break-word;
  overflow: hidden;
  padding-left: 0px;
}
.container div {
  /*background-color: aqua;*/
  margin: 20px 13.5px 13.5px 20px;
}
.container img{
  width: 100px;
  height: 120px;
  vertical-align: top;
}
</style>
