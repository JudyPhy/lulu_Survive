<template>
  <div class="container">
    <img :src=info.img>
    <div class="title">
      <span ref="name">{{info.name| ellipsis_name}}</span>
    </div>
    <div class="desc">
      <span>{{detail| ellipsis_desc}}</span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'item_poster',
  props: {
    info: Object
  },
  data: function () {
    return {
      detail: '导演：xxx|主演：xxx，xxx，xxx|简介：啊啊啊啊啊啊啊'
    }
  },
  filters: {
    ellipsis_name (value) {
      if (!value) return ''
      if (value.length > 7) {
        return value.slice(0, 7) + '...'
      }
      return value
    },
    ellipsis_desc (value) {
      if (!value) return ''
      if (value.length > 9) {
        return value.slice(0, 9) + '...'
      }
      return value
    }
  }
}
</script>

<style scoped>
.container {
  width: 100%;
  height: 100%;
  position: relative;
}
.container img {
  width: 100%;
  height: 80%;
  position: absolute;
  left: 0;
  top: 0;
}
.title {
  /*background-color: chocolate;*/
  width: 100%;
  height: 10%;
  text-align: left;
  font-size: 12px;
  color: darkslategrey;
  position: absolute;
  top: 80%;
  left: 0;
}
.title span {
  position: absolute;
  top: 0;
  left: 0;
}
.desc {
  /*background-color: blue;*/
  width: 100%;
  height: 10%;
  text-align: left;
  font-size: 8px;
  color: dimgray;
  position: absolute;
  top: 90%;
  left: 0;
}
.desc span {
  position: absolute;
  top: 0;
  left: 0;
}
</style>
