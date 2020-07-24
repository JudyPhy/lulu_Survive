<template>
  <div class="container" ref="wrapper">
    <ul ref="tab">
      <li v-for="(item,index) in list" :key="index" @click="selectedItem(item,index)">
        <one-vedio :vedioUrl="item.url"></one-vedio>-->
        <p>{{item.name}}</p>
      </li>
    </ul>
  </div>
</template>

<script>
import SingleVedio from './singleVedio'
import Bscroll from 'better-scroll'
export default {
  name: 'vedioList',
  components: {
    'one-vedio': SingleVedio
  },
  props: {
    vedioArray: Object
  },
  data: function () {
    return {
      list: this.vedioArray,
      tabWidth: 120
    }
  },
  methods: {
    initMenu: function () {
      let width = this.list.length * this.tabWidth
      this.$refs.tab.style.width = width + 'px'
      // 异步函数，确保DOM已经渲染
      this.$nextTick(() => {
        if (!this.scroll) {
          this.scroll = new Bscroll(this.$refs.wrapper, {
            click: true,
            scrollY: true
            // eventPassThrough: 'hor'
          })
        } else {
          this.scroll.refresh()
        }
      })
    },
    selectedItem: function (item, index) {
      console.log('index:', index)
    }
  },
  mounted () {
    this.$nextTick(() => {
      this.initMenu()
    })
  }
}
</script>

<style scoped>
.container{
  display: grid;
  grid-template-columns: 30% 30% 30%;
  grid-column-gap: 10px;
  grid-row-gap: 10px;
  left: 0;
  top: 0;
  overflow: hidden;
  font-size: 1rem;
}
.container ul{
  list-style: none;
}
.container li {
  display: inline-block;
  width: 120px;
}
</style>
