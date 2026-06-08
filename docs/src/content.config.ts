import { defineCollection, z } from 'astro:content';
import { glob } from 'astro/loaders';

const extensions = defineCollection({
  loader: glob({ pattern: '**/*.md', base: './src/content/extensions' }),
  schema: z.object({
    title: z.string(),
    description: z.string(),
    targetType: z.string(),
    namespace: z.string().default('Chet.Utils'),
    className: z.string(),
    category: z.string(),
    order: z.number(),
  }),
});

const helpers = defineCollection({
  loader: glob({ pattern: '**/*.md', base: './src/content/helpers' }),
  schema: z.object({
    title: z.string(),
    description: z.string(),
    namespace: z.string().default('Chet.Utils.Helpers'),
    className: z.string(),
    category: z.string(),
    order: z.number(),
  }),
});

export const collections = { extensions, helpers };
