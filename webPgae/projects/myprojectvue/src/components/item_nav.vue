<template>
  <div class="item_nav">
    <div :style="bgStyle(actived)"></div>
    <span :style="activedStyle(actived)">{{this.title}}</span>
  </div>
</template>

<script>
export default {
  name: 'item_nav',
  props: {
    name: String,
    bgColor: String,
    actived: Boolean
  },
  data: function () {
    return {
      title: this.name,
      bg_color: this.bgColor
    }
  },
  methods: {
    activedStyle: function (selected) {
      let stSelected = {
        color: 'red',
        'font-size': '20px',
        position: 'absolute',
        left: '14.7px',
        top: '22px'
      }
      let stUnselected = {
        color: 'white',
        'font-size': '16px',
        position: 'absolute',
        left: '14.7px',
        top: '31px'
      }
      return selected ? stSelected : stUnselected
    },
    bgStyle: function (actived) {
      let stSelected = {
        'background-color': this.bg_color,
        width: '69.4px',
        height: '64px',
        'margin-top': '0px'
      }
      let stUnselected = {
        'background-color': this.bg_color,
        width: '69.4px',
        height: '50px',
        'margin-top': '14px'
      }
      return actived ? stSelected : stUnselected
    }
  }
}
</script>

<style scoped>
.item_nav {
  padding: 0;
  margin: 0;
  position: relative;
}
</style>
