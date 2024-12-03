import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.css'],
})
export class TreeNodeComponent {
  @Input() node: any;

  getColor(isConcept?: boolean): string {
    return isConcept ? 'green' : 'blue';
  }
}
