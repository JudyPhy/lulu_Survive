<template>
  <div class="container">
    <img :src=info.img_src>
    <div class="title">
      {{info.title| ellipsis_name}}
    </div>
    <div class="desc">
      {{info.tag}}
    </div>
  </div>
</template>

<script>
export default {
  name: 'item_poster',
  props: {
    info: Object
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
      if (value.length > 12) {
        return value.slice(0, 12) + '...'
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
  width: 80px;
  height: 118px;
  position: absolute;
  left: 12.5px;
  top: 0;
}
.title {
  /*background-color: chocolate;*/
  width: 100%;
  height: 10%;
  text-align: center;
  font-size: 12px;
  color: darkslategrey;
  position: absolute;
  top: 118px;
  left: 0;
}
.desc {
  /*background-color: blue;*/
  width: 100%;
  height: 10%;
  text-align: center;
  font-size: 9px;
  color: dimgray;
  position: absolute;
  top: 132px;
  left: 0;
}
</style>
