<template>
  <div class="menu_top">
    <div v-for="(item,index) in tabs" :key="index" @click="selectedItem(item,index)">
      <ItemNav :name="item.title" :bgColor="item.color" :actived="index===activedIndex">
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
    }
  }
}
</script>

<style scoped>
.menu_top {
  width: 100%;
  height: 100%;
  overflow: hidden;
  font-size: 0;
  padding: 0;
  margin: 0;
}
.menu_top div {
  display: inline-block;
  overflow: hidden;
  padding: 0;
  margin: 0;
  width: 69.4px;
  height: 64px;
}
</style>
