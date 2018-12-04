import * as React from 'react'

import { Upgrades } from '../../models/Upgrades'

import './UpgradesDisplay.css'

const UpgradesDisplay = ({ numResource }) => (
	<section>
		<h2>Upgrades:</h2>
		{Object.getOwnPropertyNames(Upgrades)
			.map(upgradeKey => {
				const upgrade = Upgrades[upgradeKey]
				const { cost, name } = upgrade

				return (
					<article className="upgrade-container" key={name.replace(' ', '-')}>
						<h3>{name}</h3>
						<p>Price: {cost} resources</p>
					</article>
				)
			}
		)}
	</section>
)

export { UpgradesDisplay }
