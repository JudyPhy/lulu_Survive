<template>
  <div class="menu_top" :style="{width:menuWidth + 'px', height:menuHeight + 'px'}">
    <div v-for="(item,index) in tabs" :key="index" @click="selectedItem(item,index)"
    :style="{width:'20%', height:'100%'}">
      <ItemNav :posX="getPosX(index)" :name="item.title" :bgColor="item.color" :actived="index===activedIndex">
      </ItemNav>
    </div>
    <div></div>
  </div>
</template>

<script>
import ItemNav from './item_nav'
export default {
  name: 'menu_top',
  components: {ItemNav},
  props: {
    menuWidth: Number,
    menuHeight: Number
  },
  data: function () {
    return {
      tabs: [{
        to: '/page_tv',
        title: '影视',
        color: '#ff9914'
      },
      {
        to: '/page_episodes',
        title: '剧集',
        color: '#2c9121'
      },
      {
        to: '/page_show',
        title: '综艺',
        color: '#1ea1dc'
      },
      {
        to: '/page_anime',
        title: '动漫',
        color: '#882087'
      },
      {
        to: '/page_fav',
        title: '收藏',
        color: '#d61900'
      }],
      activedIndex: 0
    }
  },
  methods: {
    selectedItem: function (item, index) {
      this.$router.push({ path: item.to })
      this.activedIndex = index
    },
    getPosX: function (index) {
      let posX = index * this.menuWidth / 5
      return posX
    }
  }
}
</script>

<style scoped>
.menu_top {
  background-color: #2c3e50;
  overflow: hidden;
  padding: 0;
  margin: 0;
  font-size: 0;
}
.menu_top div {
  background-color: chocolate;
  display: inline-block;
  padding: 0;
  margin-top: 0;
  overflow: hidden;
}
</style>
