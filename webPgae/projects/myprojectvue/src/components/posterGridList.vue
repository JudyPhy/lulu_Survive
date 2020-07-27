<template>
  <div class="container" :style="this.style_phone">
    <p v-for="(item,index) in list" :key="index" @click="selectedItem(item.url)">
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
      list: this.vedioInfoArray,
      style_phone: {
        'grid-template-columns': 'repeat(auto-fill, 100px)'
      }
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
  display: grid;
  grid-template-columns: repeat(3, 30%);
  grid-column-gap: 1rem;
  font-size: 1rem;
  word-wrap: break-word;
  /*padding-left: 10px;*/
}
.container img{
  width: 100px;
  height: 120px;
  vertical-align: top;
}
</style>
