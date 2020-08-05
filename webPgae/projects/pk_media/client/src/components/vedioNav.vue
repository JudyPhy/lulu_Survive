<template>
  <div class="navigation" ref="wrapper">
    <ul ref="tab">
      <li v-for="(item,index) in tabs" :key="index" @click="selectedItem(item,index)">
        <ItemNav :class="{'item-normal':navIndex !== index, 'item-active':navIndex === index}" :name="item.title" :bgColor="item.color">
        </ItemNav>
      </li>
    </ul>
    <router-view/>
  </div>
</template>

<script>
import Bscroll from 'better-scroll'
import ItemNav from './item_nav'
export default {
  name: 'vedioNav',
  components: {ItemNav},
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
      navIndex: 0,
      tabWidth: 120,
      windowWidth: document.documentElement.clientWidth, // 实时屏幕宽度
      windowHeight: document.documentElement.clientHeight // 实时屏幕高度
    }
  },
  mounted () {
    this.$nextTick(() => {
      this.initMenu()
    })
  },
  methods: {
    initMenu: function () {
      let tabsWidth = this.tabWidth
      let width = this.tabs.length * tabsWidth
      this.$refs.tab.style.width = width + 'px' // `${width}px`
      // 异步函数，确保DOM已经渲染
      this.$nextTick(() => {
        if (!this.scroll) {
          this.scroll = new Bscroll(this.$refs.wrapper, {
            click: true,
            scrollX: true,
            eventPassThrough: 'vertical'
          })
        } else {
          this.scroll.refresh()
        }
      })
    },
    selectedItem: function (item, index) {
      this.$router.push({ path: item.to })
      this.navIndex = index
    }
  }
}
</script>

<style>
.navigation{
  width: 100%;
  height: 5%;
  /*background-color: bisque;*/
  position: absolute;
  left: 0;
  top: 0;
  overflow: hidden;
  font-size: 1rem;
}
.navigation ul{
  list-style: none;
}
.navigation li {
  display: inline-block;
  width: 120px;
  height: 20%;
  padding-bottom: 1.5rem;
}
.item-normal {
  width: 20%;
}
.item-active {
  width: 25%;
}
</style>
