import { createMDX } from 'fumadocs-mdx/next';
import { achNextConfig } from 'ach-fumadocs-theme/next';

const withMDX = createMDX();

// basePath = '/reactive' → docs.somiri.dev/reactive 경로에 매핑된다.
export default withMDX(achNextConfig({ repo: 'reactive' }));
