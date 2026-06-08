<template>
  <div>
    <!-- Search button -->
    <button
      @click="open = true"
      class="flex items-center gap-2 px-3 py-1.5 text-sm text-gray-500 dark:text-gray-400 bg-gray-100 dark:bg-gray-800 rounded-lg hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors w-full sm:w-48"
    >
      <svg class="w-4 h-4 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
        <path stroke-linecap="round" stroke-linejoin="round" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
      </svg>
      <span class="truncate">搜索文档...</span>
      <kbd class="hidden sm:inline ml-auto text-xs bg-gray-200 dark:bg-gray-700 px-1.5 py-0.5 rounded">⌘K</kbd>
    </button>

    <!-- Dialog -->
    <Teleport to="body">
      <div v-if="open" class="fixed inset-0 z-[100]">
        <div class="fixed inset-0 bg-black/50 backdrop-blur-sm" @click="open = false"></div>
        <div class="fixed inset-x-4 top-[10%] sm:inset-x-auto sm:left-1/2 sm:-translate-x-1/2 sm:w-full sm:max-w-xl">
          <div class="bg-white dark:bg-gray-900 rounded-xl shadow-2xl overflow-hidden border border-gray-200 dark:border-gray-700">
            <div class="flex items-center gap-3 px-4 border-b border-gray-200 dark:border-gray-700">
              <svg class="w-5 h-5 text-gray-400 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                <path stroke-linecap="round" stroke-linejoin="round" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
              <input
                ref="searchInput"
                v-model="query"
                @input="search"
                placeholder="搜索方法、类名..."
                class="flex-1 py-4 bg-transparent text-gray-900 dark:text-white outline-none placeholder:text-gray-400"
              />
              <button @click="open = false" class="text-xs text-gray-400 bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">ESC</button>
            </div>
            <div class="max-h-80 overflow-y-auto">
              <div v-if="results.length === 0 && query" class="px-4 py-8 text-center text-gray-500 dark:text-gray-400 text-sm">
                未找到相关结果
              </div>
              <div v-else-if="results.length === 0" class="px-4 py-8 text-center text-gray-500 dark:text-gray-400 text-sm">
                输入关键词开始搜索
              </div>
              <ul v-else>
                <li v-for="result in results" :key="result.url">
                  <a :href="result.url" class="block px-4 py-3 hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors" @click="open = false">
                    <div class="text-sm font-medium text-gray-900 dark:text-white" v-html="result.title"></div>
                    <div v-if="result.excerpt" class="text-xs text-gray-500 dark:text-gray-400 mt-1 line-clamp-2" v-html="result.excerpt"></div>
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </Teleport>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue';

const open = ref(false);
const query = ref('');
const results = ref([]);
const searchInput = ref(null);
let pagefindSearch = null;

onMounted(async () => {
  // Load Pagefind at runtime (not at build time)
  if (typeof window !== 'undefined') {
    try {
      const path = new URL(import.meta.env.BASE_URL + 'pagefind/pagefind.js', window.location.origin).href;
      pagefindSearch = await import(/* @vite-ignore */ path);
    } catch (e) {
      // Pagefind not available in dev mode
    }
  }

  // Keyboard shortcut
  const handleKeydown = (e) => {
    if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
      e.preventDefault();
      open.value = !open.value;
      if (open.value) {
        setTimeout(() => searchInput.value?.focus(), 100);
      }
    }
    if (e.key === 'Escape') {
      open.value = false;
    }
  };
  window.addEventListener('keydown', handleKeydown);
  onUnmounted(() => window.removeEventListener('keydown', handleKeydown));
});

async function search() {
  if (!pagefindSearch || !query.value) {
    results.value = [];
    return;
  }
  const search_result = await pagefindSearch.search(query.value);
  const data = await Promise.all(search_result.results.slice(0, 8).map(r => r.data()));
  results.value = data.map(d => ({
    url: d.url,
    title: d.meta?.title || '',
    excerpt: d.excerpt || '',
  }));
}
</script>
