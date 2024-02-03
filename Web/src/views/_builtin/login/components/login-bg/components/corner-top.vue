<template>
  <svg height="1337" width="1337">
    <defs>
      <path
        id="path-1"
        opacity="1"
        fill-rule="evenodd"
        d="M257.25 459.896c-95.038 64.972-152.25 171.687-152.25 290.103 0 188.182 152.317 340.5 340.5 340.5 119.204 0 225.865-59.96 290.83-150.95-15.727-81.512-84.535-143.79-169.33-143.79-43.992 0-84.271 16.698-114.974 44.212-30.704-27.513-71.045-44.211-114.037-44.211-84.795 0-153.603 62.277-169.33 143.79 64.965 90.992 171.626 150.952 290.83 150.952 188.183 0 340.5-152.318 340.5-340.5 0-118.416-57.212-225.13-152.25-290.102zM474.75 946.5c-119.206 0-225.866 59.96-290.83 150.95 15.727 81.512 84.535 143.789 169.33 143.789 43.992 0 84.271-16.698 114.974-44.211 30.704 27.513 71.045 44.211 114.037 44.211 84.795 0 153.603-62.277 169.33-143.789-64.965-90.99-171.626-150.95-290.83-150.95z"
      />
      <linearGradient id="linearGradient-2" x1="0.79" y1="0.62" x2="0.21" y2="0.86">
        <stop offset="0" :stop-color="startColor" stop-opacity="1" />
        <stop offset="1" :stop-color="endColor" stop-opacity="1" />
      </linearGradient>
    </defs>
    <g opacity="1">
      <use xlink:href="#path-1" fill="url(#linearGradient-2)" fill-opacity="1" />
    </g>
  </svg>
</template>

<script lang="ts" setup>
interface Props {
  /** 过渡的开始颜色 */
  startColor?: string;
  /** 过渡的结束颜色 */
  endColor?: string;
}

withDefaults(defineProps<Props>(), {
  startColor: '#28aff0',
  endColor: '#120fc4'
});
</script>

<style scoped></style>
