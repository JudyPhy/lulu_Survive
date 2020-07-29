<template>
  <div class="item_nav" :style="activedStyle(actived)">
    {{this.title}}
  </div>
</template>

<script>
export default {
  name: 'item_nav',
  props: {
    posX: Number,
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
        'background-color': this.bg_color,
        width: '100%',
        height: '100%',
        color: 'red'
      }
      let stUnselected = {
        'background-color': this.bg_color,
        width: '100%',
        height: '100%',
        color: 'white'
      }
      return selected ? stSelected : stUnselected
    }
  }
}
</script>

<style scoped>
.item_nav {
  padding: 0;
  margin: 0;
  font-size: 1rem;
  color: white;
}
</style>
