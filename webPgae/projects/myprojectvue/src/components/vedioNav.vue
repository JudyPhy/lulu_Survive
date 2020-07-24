<template>
  <div class="navigation" ref="wrapper">
    <ul ref="tab">
      <li v-for="(item,index) in tabs" :key="index" @click="selectedItem(item,index)">
        <p :class="navIndex === index ? 'item-cn item-cn-active' : 'item-cn'">
          {{item.title}}
        </p>
      </li>
    </ul>
  </div>
</template>

<script>
import Bscroll from 'better-scroll'
export default {
  name: 'vedioNav',
  data: function () {
    return {
      tabs: [{
        to: '/page_recommend',
        title: '推荐'
      },
      {
        to: '/page_2',
        title: '电影'
      },
      {
        to: '/vedioList',
        title: '电视剧'
      },
      {
        to: '/page_3',
        title: '综艺'
      },
      {
        to: '/vedioList',
        title: '动漫'
      },
      {
        to: '/vedioList',
        title: '游戏'
      }],
      navIndex: 0,
      tabWidth: 120,
      imgUrl: require('../assets/logo.png'),
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
  background-color: bisque;
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
  padding-bottom: 1.5rem;
}
.navigation a {
  color: #fff;
  text-decoration: none;
}
.navigation a:hover {
  background-color: #2c3e50;
}
.item-cn {
  color: #1c2438;
  font-weight: 800;
}
.item-cn-active {
  color: #2d8cf0;
}
</style>
