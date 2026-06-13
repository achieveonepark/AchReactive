import { createMDX } from 'fumadocs-mdx/next';
import { achNextConfig } from 'ach-fumadocs-theme/next';

const withMDX = createMDX();

// basePath = '/AchReactive' → docs.somiri.dev/AchReactive 경로에 매핑된다 (애그리게이터 서빙 경로 = 레포명).
export default withMDX(achNextConfig({ repo: 'AchReactive' }));
