<template>
  <div class="container">
    <p v-for="(item,index) in vedioInfoArray" :key="index" @click="selectedItem(item.url)">
      <img :src="item.img">
      <br>{{item.name| ellipsis}}
    </p>
  </div>
</template>

<script>
export default {
  name: 'posterGridList',
  props: {
    vedioInfoArray: Array
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
      let routerUrl = this.$router.resolve({
        path: '/singleVedio',
        query: {id: 1}
      })
      window.open(routerUrl.href, '_blank')
    }
  }
}
</script>

<style>
.container{
  background-color: darkgray;
  display: grid;
  grid-template-columns: repeat(3, 30%);
  grid-template-rows: repeat(3, 30%);
  grid-column-gap: 3%;
  grid-row-gap: 2%;
  font-size: 1rem;
  word-wrap: break-word;
  overflow: hidden;
  padding: 0 0 0 1%;
  margin: 0;
}
.container img{
  width: 100px;
  height: 120px;
  vertical-align: top;
}
</style>
