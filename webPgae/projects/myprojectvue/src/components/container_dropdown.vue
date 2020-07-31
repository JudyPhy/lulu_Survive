<template>
  <div class="container" @touchstart="touchstart" @touchmove="touchMove" @touchend="touchEnd" :style="{top:offsetY + 'px', transform: 'translate3d(0,${ani_offsetY}px, 0)'}">
    <div class="refresh">
      <span>{{this.message}}</span>
    </div>
    <div class="content">
<!--      <router-view/>-->
    </div>
  </div>
</template>

<script>
export default {
  name: 'container_dropdown',
  data: function () {
    return {
      message: '下拉即可刷新',
      offsetY: -64,
      offsetY_max: 0,
      startY: 0,
      moveState: 0, // 0: 下拉即可刷新; 1:释放即可刷新; 2:加载中
      ani_offsetY: 0,
      timer: ''
    }
  },
  methods: {
    touchstart: function (e) {
      if (this.moveState === 2) {
        return
      }
      this.startY = e.targetTouches[0].clientY
    },
    touchMove: function (e) {
      if (this.moveState === 2) {
        return
      }
      e.preventDefault()
      let move = e.targetTouches[0].clientY - this.startY
      this.startY = e.targetTouches[0].clientY
      // update container offsetY
      let curOffsetY = this.offsetY
      if (move >= 0) {
        curOffsetY = this.offsetY + Math.pow(move, 0.6)
      } else {
        curOffsetY = this.offsetY - Math.pow(-move, 0.6)
      }
      if (curOffsetY >= this.offsetY_max) {
        curOffsetY = this.offsetY_max
      }
      this.offsetY = curOffsetY
      // update current state
      if (this.offsetY > -20) {
        this.moveState = 1
        this.message = '释放即可刷新'
      } else {
        this.moveState = 0
        this.message = '下拉即可刷新'
      }
    },
    touchEnd: function (e) {
      if (this.moveState === 0) {
        this.offsetY = -64
      } else if (this.moveState === 1) {
        this.moveState = 2
        this.message = '加载中...'
        this.startTimer()
      }
      this.ani_offsetY = Math.abs(this.offsetY)
    },
    startTimer: function () {
      this.timer = setTimeout(this.hideRefresh, 3000)
    },
    hideRefresh: function () {
      clearTimeout(this.timer)
      this.offsetY = -64
      this.moveState = 0
    }
  }
}
</script>

<style scoped>
.container {
  position: absolute;
  width: 100%;
  height: 100%;
  left: 0;
  bottom:0;
  right:0;
}
.refresh {
  background-color: brown;
  width: 347px;
  height: 64px;
  position: relative;
}
.refresh span {
  font-size: 14px;
  color: white;
  position: absolute;
  width: 347px;
  bottom: 25px;
  left: 0;
}
</style>
