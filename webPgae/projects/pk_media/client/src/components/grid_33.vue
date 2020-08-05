<template>
  <div class="container">
    <div class="item" v-for="(item,index) in dataArray" :key="index" @click="selectedItem(item.data_id)">
      <Item :info="item"></Item>
    </div>
  </div>
</template>

<script>
import Item from './item_poster'
export default {
  name: 'grid_33',
  props: {
    dataArray: Array
  },
  components: {Item},
  methods: {
    selectedItem: function (dataId) {
      this.$router.push({
        path: '/page_vedio_play',
        query: {
          dataId: dataId
        }
      })
    }
  }
}
</script>

<style scoped>
.container {
  /*background-color: blueviolet;*/
  width: 347px;
  height: 480px;
  top: 5px;
  overflow: hidden;
  word-wrap: break-word;
  font-size: 16px;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
}
.item {
  /*background-color: blueviolet;*/
  width: 105px;
  height: 150px;
  color: white;
  flex: 0 0 auto;
  overflow: hidden;
}
</style>
