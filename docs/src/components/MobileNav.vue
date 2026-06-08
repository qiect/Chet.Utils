<template>
  <div class="lg:hidden">
    <button
      @click="open = !open"
      class="p-2 rounded-md text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
    >
      <svg v-if="!open" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
        <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16" />
      </svg>
      <svg v-else class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
        <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
      </svg>
    </button>

    <Teleport to="body">
      <div v-if="open" class="fixed inset-0 z-50 lg:hidden">
        <div class="fixed inset-0 bg-black/50" @click="open = false"></div>
        <div class="fixed inset-y-0 left-0 w-72 bg-white dark:bg-gray-950 shadow-xl overflow-y-auto">
          <div class="flex items-center justify-between p-4 border-b border-gray-200 dark:border-gray-800">
            <span class="text-lg font-bold text-gray-900 dark:text-white">Chet.Utils</span>
            <button @click="open = false" class="p-2 rounded-md text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800">
              <svg class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
          <nav class="p-4 space-y-6">
            <div>
              <a :href="base + 'getting-started'" class="block px-3 py-1.5 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-md" @click="open = false">快速开始</a>
            </div>
            <div>
              <h3 class="px-3 mb-2 text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase tracking-wider">扩展方法</h3>
              <ul class="space-y-0.5">
                <li v-for="ext in extensions" :key="ext.slug">
                  <a :href="base + 'extensions/' + ext.slug" class="flex items-center gap-2 px-3 py-1.5 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-md" @click="open = false">
                    <span class="text-xs text-gray-400 dark:text-gray-500 font-mono">{{ ext.target }}</span>
                    <span>{{ ext.name.replace('Extensions', '') }}</span>
                  </a>
                </li>
              </ul>
            </div>
            <div>
              <h3 class="px-3 mb-2 text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase tracking-wider">帮助类</h3>
              <ul class="space-y-0.5">
                <li v-for="h in helpers" :key="h.slug">
                  <a :href="base + 'helpers/' + h.slug" class="block px-3 py-1.5 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-md truncate" @click="open = false">
                    {{ h.name }}
                  </a>
                </li>
              </ul>
            </div>
          </nav>
        </div>
      </div>
    </Teleport>
  </div>
</template>

<script setup>
import { ref } from 'vue';

const open = ref(false);
const base = import.meta.env.BASE_URL;

const extensions = [
  { name: 'BoolExtensions', slug: 'bool-extensions', target: 'bool' },
  { name: 'DataTableExtensions', slug: 'datatable-extensions', target: 'DataTable' },
  { name: 'DateTimeExtensions', slug: 'datetime-extensions', target: 'DateTime' },
  { name: 'DecimalExtensions', slug: 'decimal-extensions', target: 'decimal' },
  { name: 'DoubleExtensions', slug: 'double-extensions', target: 'double' },
  { name: 'EnumExtensions', slug: 'enum-extensions', target: 'Enum' },
  { name: 'EnumerableExtensions', slug: 'enumerable-extensions', target: 'IEnumerable' },
  { name: 'FileExtensions', slug: 'file-extensions', target: 'FileInfo' },
  { name: 'FloatExtensions', slug: 'float-extensions', target: 'float' },
  { name: 'IntExtensions', slug: 'int-extensions', target: 'int' },
  { name: 'StreamExtensions', slug: 'stream-extensions', target: 'Stream' },
  { name: 'StringExtensions', slug: 'string-extensions', target: 'string' },
];

const helpers = [
  { name: 'ApplicationHelper', slug: 'application-helper' },
  { name: 'DataTableHelper', slug: 'datatable-helper' },
  { name: 'DataTableMyHelper', slug: 'datatable-my-helper' },
  { name: 'FileHelper', slug: 'file-helper' },
  { name: 'HttpClientHelper', slug: 'httpclient-helper' },
  { name: 'ReflectHelper', slug: 'reflect-helper' },
  { name: 'RegexHelper', slug: 'regex-helper' },
  { name: 'StopwatchHelper', slug: 'stopwatch-helper' },
  { name: 'TaskHelper', slug: 'task-helper' },
  { name: 'UnitHelper', slug: 'unit-helper' },
  { name: 'WebSocketHelper', slug: 'websocket-helper' },
];
</script>
