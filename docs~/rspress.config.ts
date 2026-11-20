import { defineConfig } from '@rspress/core';
import { fileURLToPath } from 'node:url';

export default defineConfig({
  root: 'content/docs',
  base: '/AchReactive/',
  outDir: 'out',
  lang: 'ko',
  title: 'AchReactive',
  description:
    '데이터 조합으로 모든 게임 콘텐츠를 만드는 도메인 무관 리액션 프레임워크.',
  icon: '/logo.svg',
  logo: '/logo.svg',
  logoText: 'AchReactive',
  logoHref: '/ko/',
  globalStyles: fileURLToPath(new URL('./theme/index.css', import.meta.url)),
  themeConfig: {
    darkMode: 'dark',
    nav: [
      {
        text: '언어',
        items: [
          { text: '한국어', link: '/ko/' },
          { text: 'English', link: '/en/' },
          { text: '日本語', link: '/ja/' },
          { text: '简体中文', link: '/zh/' },
        ],
      },
    ],
    sidebar: {
      '/ko/': [
        {
          text: '문서',
          items: [
            { text: '개요', link: '/ko/' },
            { text: '시작하기', link: '/ko/getting-started' },
            { text: 'API 레퍼런스', link: '/ko/api' },
            { text: '변경 이력', link: '/ko/changelog' },
          ],
        },
        {
          text: '가이드',
          items: [
            { text: '핵심 컨셉', link: '/ko/usage/concepts' },
            { text: '데이터와 소스', link: '/ko/usage/data-sources' },
            { text: '리액션과 기본 동작', link: '/ko/usage/reactions' },
            { text: '확장하기', link: '/ko/usage/extending' },
            { text: '저작 도구', link: '/ko/usage/authoring-tools' },
          ],
        },
      ],
      '/en/': [
        {
          text: 'Docs',
          items: [
            { text: 'Overview', link: '/en/' },
            { text: 'Getting started', link: '/en/getting-started' },
            { text: 'API reference', link: '/en/api' },
            { text: 'Changelog', link: '/en/changelog' },
          ],
        },
        {
          text: 'Guide',
          items: [
            { text: 'Core concepts', link: '/en/usage/concepts' },
            { text: 'Data and sources', link: '/en/usage/data-sources' },
            {
              text: 'Reactions and default behaviors',
              link: '/en/usage/reactions',
            },
            { text: 'Extending', link: '/en/usage/extending' },
            { text: 'Authoring tools', link: '/en/usage/authoring-tools' },
          ],
        },
      ],
      '/ja/': [
        {
          text: 'ドキュメント',
          items: [
            { text: '概要', link: '/ja/' },
            { text: 'はじめに', link: '/ja/getting-started' },
            { text: 'API リファレンス', link: '/ja/api' },
            { text: '変更履歴', link: '/ja/changelog' },
          ],
        },
        {
          text: 'ガイド',
          items: [
            { text: 'コアコンセプト', link: '/ja/usage/concepts' },
            { text: 'データとソース', link: '/ja/usage/data-sources' },
            { text: 'リアクションと基本動作', link: '/ja/usage/reactions' },
            { text: '拡張する', link: '/ja/usage/extending' },
            { text: 'オーサリングツール', link: '/ja/usage/authoring-tools' },
          ],
        },
      ],
      '/zh/': [
        {
          text: '文档',
          items: [
            { text: '概览', link: '/zh/' },
            { text: '开始使用', link: '/zh/getting-started' },
            { text: 'API 参考', link: '/zh/api' },
            { text: '变更历史', link: '/zh/changelog' },
          ],
        },
        {
          text: '指南',
          items: [
            { text: '核心概念', link: '/zh/usage/concepts' },
            { text: '数据与来源', link: '/zh/usage/data-sources' },
            { text: '响应与基础动作', link: '/zh/usage/reactions' },
            { text: '扩展', link: '/zh/usage/extending' },
            { text: '创作工具', link: '/zh/usage/authoring-tools' },
          ],
        },
      ],
    },
    socialLinks: [
      {
        icon: 'github',
        mode: 'link',
        content: 'https://github.com/achieveonepark/AchReactive',
      },
    ],
  },
});
